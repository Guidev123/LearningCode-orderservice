using MediatR;
using OrderService.Application.Queries.GetAllOrders;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsHandler(IProductRepository productRepository)
                                     : IRequestHandler<GetAllProductsQuery, PagedResponse<List<Product>>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<PagedResponse<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (products is null)
                return new PagedResponse<List<Product>>(null, 404, "Erro: Nao foi possivel encontrar produtos");

            var pagedProducts = products.Skip((request.PageNumber - 1) * request.PageSize)
                              .Take(request.PageSize).ToList();

            var count = products.Count();

            return new PagedResponse<List<Product>>(pagedProducts, count, request.PageNumber, request.PageSize);
        }
    }
}
