using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Application.Commands.CreateOrder
{
    public class CreateOrderHandler(IOrderRepository orderRepository,
                                    IProductRepository productRepository,
                                    IVoucherRepository voucherRepository)
               : IRequestHandler<CreateOrderCommand, Response<OrderDTO?>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        public async Task<Response<OrderDTO?>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderProduct = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (orderProduct is null)
                return new Response<OrderDTO?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            var voucher = await ValidateVoucherAsync(request.VoucherId);
            if (!voucher.IsSuccess)
                return new Response<OrderDTO?>(null, 404, voucher.Message);

            var order = new Order(request.UserId, request.ProductId, orderProduct,
                                  orderProduct.Price - (voucher.Data?.Amount ?? 0), voucher.Data, request.VoucherId);

            await _orderRepository.CreateOrderAsync(order);

            var result = OrderDTO.MapFromEntity(order);
            return new Response<OrderDTO?>(result, 201, ResponseMessages.ORDER_CREATED_SUCCESS.GetDescription());
        }

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
    }
}
