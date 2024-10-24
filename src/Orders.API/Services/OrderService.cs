﻿using Microsoft.Extensions.Options;
using Orders.Domain.Entities;
using Orders.Domain.Enums;
using Orders.Domain.Interfaces.ExternalServices;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;
using Orders.Domain.Response.Messages;
using Orders.Infrastructure.MessageBus;
using Orders.Infrastructure.MessageBus.Configuration;
using Orders.Infrastructure.MessageBus.Messages;

namespace Orders.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStripeService _stripeService;
        private readonly IMessageBusClient _bus;
        private readonly BusSettingsConfiguration _busSettings;
        public OrderService(IOrderRepository orderRepository,
                                  IVoucherRepository voucherRepository,
                                  IProductRepository productRepository,
                                  IStripeService stripeService,
                                  IMessageBusClient bus,
                                  IOptions<BusSettingsConfiguration> busSettings)
        {
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
            _productRepository = productRepository;
            _stripeService = stripeService;
            _bus = bus;
            _busSettings = busSettings.Value;
        }

        public async Task<Response<Order?>> CancelOrderAsync(CancelOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
                return new Response<Order?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Canceled:
                    return new Response<Order?>(null, 400, ResponseMessages.ORDER_ALREADY_CANCELED.GetDescription());
                case EOrderStatus.Refunded:
                    return new Response<Order?>(null, 400, ResponseMessages.ORDER_ALREADY_REFUNDED.GetDescription());
                case EOrderStatus.Paid:
                    return new Response<Order?>(null, 400, ResponseMessages.ORDER_ALREADY_PAID.GetDescription());
                default:
                    return new Response<Order?>(null, 400, ResponseMessages.ORDER_CANNOT_BE_CANCELED.GetDescription());
            }

            order.CancellStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);
            return new Response<Order?>(order, 200, ResponseMessages.ORDER_CANCELED_SUCCESS.GetDescription());
        }

        public async Task<Response<Order?>> CreateOrderAsync(CreateOrderRequest request)
        {
            var orderProduct = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (orderProduct is null)
                return new Response<Order?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            var voucher = await ValidateVoucherAsync(request.VoucherId);
            if (!voucher.IsSuccess)
                return new Response<Order?>(null, 404, voucher.Message);

            var order = new Order(request.UserId,request.ProductId, orderProduct,
                                  orderProduct.Price - (voucher.Data?.Amount ?? 0), voucher.Data, request.VoucherId);

            await _orderRepository.CreateOrderAsync(order);

            return new Response<Order?>(order, 201, ResponseMessages.ORDER_CREATED_SUCCESS.GetDescription());
        }

        public async Task<Response<Order?>> PayOrderAsync(PayOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, request.UserId);
            if (order is null)
                return new Response<Order?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Paid:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_ALREADY_PAID.GetDescription());
                case EOrderStatus.Canceled:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_ALREADY_CANCELED.GetDescription());
                case EOrderStatus.Refunded:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_ALREADY_REFUNDED.GetDescription());
                default:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_CANNOT_BE_PAID.GetDescription());
            }

            var getTransactionByOrderNumberRequest = new GetTransactionByOrderNumberRequest(order.Number ?? string.Empty);

            var result = await _stripeService.GetTransactionsByOrderNumberAsync(getTransactionByOrderNumberRequest);

            if (!result.IsSuccess)
                return new Response<Order?>(null, 404, ResponseMessages.PAYMENT_NOT_FOUND.GetDescription());

            if (result.Data is null)
                return new Response<Order?>(null, 404, ResponseMessages.PAYMENT_NOT_FOUND.GetDescription());

            if (result.Data.Any(item => item.Refunded))
                return new Response<Order?>(null, 400, ResponseMessages.ORDER_ALREADY_REFUNDED_CANNOT_BE_PAID.GetDescription());

            if (!result.Data.Any(item => item.Paid))
                return new Response<Order?>(null, 400, ResponseMessages.ORDER_NOT_PAID_YET.GetDescription());

            order.PayStatusOrder(result.Data[0].Id);
            await _orderRepository.UpdateOrderAsync(order);

            _bus.Publish(new UpdateUserRoleMessage(Guid.Parse(request.UserId), true), _busSettings.RoutingKey, _busSettings.Exchange);

            return new Response<Order?>(order, 200, ResponseMessages.ORDER_PAID_SUCCESS.GetDescription());
        }

        public async Task<Response<Order?>> RefundOrderAsync(RefundOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
                return new Response<Order?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            switch (order.Status)
            {
                case EOrderStatus.Paid:
                    break;
                case EOrderStatus.WaitingPayment:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_NOT_PAID_CANNOT_BE_REFUNDED.GetDescription());
                case EOrderStatus.Canceled:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_ALREADY_CANCELED_CANNOT_BE_REFUNDED.GetDescription());
                case EOrderStatus.Refunded:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_ALREADY_REFUNDED.GetDescription());
                default:
                    return new Response<Order?>(order, 400, ResponseMessages.ORDER_CANNOT_BE_REFUNDED.GetDescription());
            }

            order.RefundStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);

            return new Response<Order?>(order, 200, ResponseMessages.ORDER_REFUNDED_SUCCESS.GetDescription());
        }

        #region Utils

        protected async Task<Response<Voucher?>> ValidateVoucherAsync(long? voucherId)
        {

            if (voucherId is null)
                return new Response<Voucher?>(null, 200);

            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);

            if (voucher is null)
                return new Response<Voucher?>(null, 404, ResponseMessages.VOUCHER_NOT_FOUND.GetDescription());

            if (voucher.IsUsed)
                return new Response<Voucher?>(null, 400, ResponseMessages.VOUCHER_INACTIVE.GetDescription());

            voucher.SetVoucherAsUsed();
            await _voucherRepository.UpdateVoucherAsync(voucher);

            return new Response<Voucher?>(voucher, 200);
        }
        #endregion
    }
}
