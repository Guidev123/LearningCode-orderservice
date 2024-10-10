using MediatR;
using OrderService.Application.Queries.GetAllOrders;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, Response<Product>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllProductsAsync();

            if (product is null)
                return new Response<Product>(product, 404, "Erro: Produtos nao foram encontrados");

            return new Response<Product>(product, 200, "Sucesso: Produtos encontrados");
        }
    }
}
