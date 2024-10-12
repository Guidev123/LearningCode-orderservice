using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CancelOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<Order>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(request.Number, request.UserId);
            if(order is null)
                return new Response<Order>(order, 404, "Erro: Pedido nao encontrado");

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;

                case EOrderStatus.Canceled:
                    return new Response<Order>(order, 400, "Alerta: Este pedido ja foi cancelado");

                case EOrderStatus.Refunded:
                    return new Response<Order>(order, 400, "Alerta: Este pedido ja foi reembolsado");

                case EOrderStatus.Paid:
                    return new Response<Order>(order, 400, "Alerta: Este pedido ja foi pago");

                default:
                    return new Response<Order>(order, 400, "Erro: Este pedido nao pode ser cancelado");

            }

            order.CancellStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);
            return new Response<Order>(order, 200, "Sucesso: Pedido cancelado");
        }
    }
}
