using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<Response<Order>>
    {
        public Guid UserId { get; set; }
    }
}
