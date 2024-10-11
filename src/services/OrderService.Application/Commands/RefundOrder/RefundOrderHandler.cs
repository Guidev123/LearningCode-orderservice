using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.RefundOrder
{
    public class RefundOrderHandler(IOrderRepository orderRepository) : IRequestHandler<RefundOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<Order>> Handle(RefundOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, request.UserId);
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
    }
}
