using MediatR;
using Microsoft.Extensions.Options;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Enums;
using Orders.Domain.Repositories;
using Orders.Infrastructure.ExternalServices;
using Orders.Infrastructure.MessageBus;
using Orders.Infrastructure.MessageBus.Configuration;
using Orders.Infrastructure.MessageBus.Messages;

namespace Orders.Application.Commands.PayOrder
{
    public class PayOrderHandler(IOrderRepository orderRepository,
                                 IStripeService stripeService,
                                 IMessageBusClient messageBusClient,
                                 IOptions<BusSettingsConfiguration> busSettings)
                               : IRequestHandler<PayOrderCommand, Response<OrderDTO?>>
    {
        private readonly IStripeService _stripeService = stripeService;
        private readonly IMessageBusClient _messageBusClient = messageBusClient;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly BusSettingsConfiguration _busSettings = busSettings.Value;

        public async Task<Response<OrderDTO?>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(request.OrderNumber, request.UserId);
            if (order is null)
                return new Response<OrderDTO?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Paid:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_PAID.GetDescription());
                case EOrderStatus.Canceled:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_CANCELED.GetDescription());
                case EOrderStatus.Refunded:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_REFUNDED.GetDescription());
                default:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_CANNOT_BE_PAID.GetDescription());
            }

            var resultStripe = await _stripeService.GetTransactionsByOrderNumberAsync(new(order.Number ?? string.Empty));

            if (resultStripe.Count <= 0)
                return new Response<OrderDTO?>(null, 404, ResponseMessages.PAYMENT_NOT_FOUND.GetDescription());

            if (resultStripe.Any(item => item.Refunded))
                return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_REFUNDED_CANNOT_BE_PAID.GetDescription());

            if (!resultStripe.Any(item => item.Paid))
                return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_NOT_PAID_YET.GetDescription());

            order.PayStatusOrder(resultStripe[0].Id);
            await _orderRepository.UpdateOrderAsync(order);

            _messageBusClient.Publish(new UpdateUserRoleMessage(Guid.Parse(request.UserId), true), _busSettings.RoutingKey, _busSettings.Exchange);

            var result = OrderDTO.MapFromEntity(order);
            return new Response<OrderDTO?>(result, 200, ResponseMessages.ORDER_PAID_SUCCESS.GetDescription());
        }
    }
}
