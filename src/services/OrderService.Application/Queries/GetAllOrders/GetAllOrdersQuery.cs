using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<Response>
    {
        public Guid UserId { get; set; }
    }
}
