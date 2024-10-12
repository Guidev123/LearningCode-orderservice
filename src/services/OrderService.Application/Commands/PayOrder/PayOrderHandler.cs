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

namespace OrderService.Application.Commands.PayOrder
{
    public class PayOrderHandler(IOrderRepository orderRepository) : IRequestHandler<PayOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<Order>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByNumberAsync(request.OrderNumber, request.UserId);
            if(order is null) 
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

            order.PayStatusOrder(order.ExternalReference);
            await _orderRepository.UpdateOrderAsync(order);

            return new Response<Order>(order, 200, $"Sucesso: Pedido {order.Number} foi pago com sucesso");
        }
    }
}
