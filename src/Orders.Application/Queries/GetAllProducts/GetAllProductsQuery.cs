using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Domain.Entities;

namespace Orders.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResponse<List<ProductDTO>?>>
    {
        public GetAllProductsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
    }
}
