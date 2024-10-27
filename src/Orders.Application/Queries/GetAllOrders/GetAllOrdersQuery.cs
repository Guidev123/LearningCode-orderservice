using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Domain.Entities;

namespace Orders.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PagedResponse<List<OrderDTO>?>>
    {
        public GetAllOrdersQuery(int pageNumber, int pageSize, string userId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            UserId = userId;
        }

        public string UserId { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}
