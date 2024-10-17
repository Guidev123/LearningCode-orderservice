using Orders.Domain.Entities;
using Orders.Domain.Enums;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;

namespace Orders.Domain.Services
{
    public class OrderService(IOrderRepository orderRepository,
                              IVoucherRepository voucherRepository,
                              IProductRepository productRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Response<Order>> CancelOrderAsync(CancelOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
                return new Response<Order>(null, 404, "Erro: Pedido nao encontrado");

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;

                case EOrderStatus.Canceled:
                    return new Response<Order>(null, 400, "Alerta: Este pedido ja foi cancelado");

                case EOrderStatus.Refunded:
                    return new Response<Order>(null, 400, "Alerta: Este pedido ja foi reembolsado");

                case EOrderStatus.Paid:
                    return new Response<Order>(null, 400, "Alerta: Este pedido ja foi pago");

                default:
                    return new Response<Order>(null, 400, "Erro: Este pedido nao pode ser cancelado");

            }

            order.CancellStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);
            return new Response<Order>(order, 200, "Sucesso: Pedido cancelado");
        }

        public async Task<Response<Order>> CreateOrderAsync(CreateOrderRequest request)
        {
            var orderProduct = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (orderProduct is null)
                return new Response<Order>(null, 404, "Erro: Produto do pedido nao encontrado");

            var voucher = await ValidateVoucherAsync(request.VoucherId);
            if (!voucher.IsSuccess)
                return new Response<Order>(null, 404, voucher.Message);

            var order = new Order(request.UserId, request.ProductId, voucher.Data, request.VoucherId);
            await _orderRepository.CreateOrderAsync(order);


            return new Response<Order>(order, 201, $"Sucesso: Pedido {order.Number} foi criado");
        }

        public async Task<Response<Order>> PayOrderAsync(PayOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, request.UserId);
            if (order is null)
                return new Response<Order>(null, 404, "Erro: Pedido nao encontrado");

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Paid:
                    return new Response<Order>(order, 400, "Erro: Pedido ja foi pago");
                case EOrderStatus.Canceled:
                    return new Response<Order>(order, 400, "Erro: Pedido ja foi cancelado, portanto nao pode ser pago");
                case EOrderStatus.Refunded:
                    return new Response<Order>(order, 400, "Erro: Pedido ja foi estornado");
                default:
                    return new Response<Order>(order, 400, "Erro: Pedido nao pode ser pago");
            }

            // INTEGRACAO STRIPE

            order.PayStatusOrder(order.ExternalReference ?? string.Empty);
            await _orderRepository.UpdateOrderAsync(order);

            return new Response<Order>(order, 200, $"Sucesso: Pedido {order.Number} foi pago com sucesso");
        }

        public async Task<Response<Order>> RefundOrderAsync(RefundOrderRequest request)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
                return new Response<Order>(order, 404, "Erro: Pedido nao encontrado");

            switch (order.Status)
            {
                case EOrderStatus.Paid:
                    break;
                case EOrderStatus.WaitingPayment:
                    return new Response<Order>(order, 400, "Erro: Pedido ainda nao foi pago, portanto nao pode ser estornado");
                case EOrderStatus.Canceled:
                    return new Response<Order>(order, 400, "Erro: Pedido ja foi cancelado, portanto nao pode ser estornado");
                case EOrderStatus.Refunded:
                    return new Response<Order>(order, 400, "Erro: Pedido ja foi estornado");
                default:
                    return new Response<Order>(order, 400, "Erro: Pedido nao pode ser pago");
            }

            order.RefundStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);

            return new Response<Order>(order, 200, "Sucesso: Pagamento estornado com sucesso");
        }

        #region Utils
        protected async Task<Response<Voucher?>> ValidateVoucherAsync(long? voucherId)
        {

            if (voucherId is null)
                return new Response<Voucher?>(null, 200);

            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);

            if (voucher is null)
                return new Response<Voucher?>(null, 404, "Erro: Esse voucher informado nao foi encontrado");

            if (voucher.IsUsed)
                return new Response<Voucher?>(null, 400, "Erro: Esse voucher nao esta ativo mais");

            voucher.SetVoucherAsUsed();
            await _voucherRepository.UpdateVoucherAsync(voucher);

            return new Response<Voucher?>(voucher, 200);
        }
        #endregion
    }
}
