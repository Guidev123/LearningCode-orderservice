using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetOrderByNumber
{
    public class GetOrderByNumberHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByNumberQuery, Response<Order>>
    {   
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<Order>> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId, request.UserId);
            if (order is null)
                return new Response<Order>(order, 404, "Erro: Pedido nao encontrado");

            return new Response<Order>(order, 200, "Sucesso: Pedido encontrado");
        }
    }
}
