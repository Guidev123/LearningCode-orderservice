using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetAllProducts
{
    public class GetAllProductsHandler(IProductRepository productRepository)
               : IRequestHandler<GetAllProductsQuery, PagedResponse<List<ProductDTO>?>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<PagedResponse<List<ProductDTO>?>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _productRepository.GetAllProducts(request.PageNumber, request.PageSize);
            if (query is null)
                return new PagedResponse<List<ProductDTO>?>(null, 404, ResponseMessages.PRODUCTS_RETRIEVAL_FAILED.GetDescription());

            var pagedProducts = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var productsTotalCount = query.Count();

            var result = pagedProducts.Select(ProductDTO.MapFromEntity).ToList();

            return new PagedResponse<List<ProductDTO>?>(result, productsTotalCount, request.PageSize, request.PageNumber);
        }
    }
}
