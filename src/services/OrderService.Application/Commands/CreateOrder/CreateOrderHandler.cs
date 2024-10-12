using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderHandler(IVoucherRepository voucherRepository,
                                    IProductRepository productRepository,
                                    IOrderRepository orderRepository)
                                    : IRequestHandler<CreateOrderCommand, Response<Order?>>
    {
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<Response<Order?>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderProduct = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (orderProduct is null)
                return new Response<Order?>(null, 404, "Erro: Produto do pedido nao encontrado");

            var voucher = await ValidateVoucherAsync(request.VoucherId);
            if (!voucher.IsSuccess)
                return new Response<Order?>(null, 404, voucher.Message);

            var order = new Order(request.UserId, request.VoucherId, request.ProductId, orderProduct, voucher.Data);
            await _orderRepository.CreateOrderAsync(order);


            return new Response<Order?>(order, 201, $"Sucesso: Pedido {order.Number} foi criado");
        }

        protected async Task<Response<Voucher?>> ValidateVoucherAsync(long? voucherId)
        {

            if (voucherId is null)
                return new Response<Voucher?>(null, 200);

            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);

            if(voucher is null)
                return new Response<Voucher?>(null, 404, "Erro: Esse voucher informado nao foi encontrado");

            if (!voucher.IsActive)
                return new Response<Voucher?>(null, 400, "Erro: Esse voucher nao esta ativo mais");

            voucher.SetVoucherAsUsed();
            await _voucherRepository.UpdateVoucherAsync(voucher);

            return new Response<Voucher?>(voucher, 200);
        }
    }
}
