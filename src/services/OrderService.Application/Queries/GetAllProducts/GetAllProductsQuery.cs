using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResponse<List<Product>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
