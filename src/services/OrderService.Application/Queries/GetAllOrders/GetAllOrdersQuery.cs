using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PagedResponse<List<Order>>>
    {
        public string UserId { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
