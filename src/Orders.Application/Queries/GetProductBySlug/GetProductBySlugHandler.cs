using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugHandler(IProductRepository productRepository) : IRequestHandler<GetProductBySlugQuery, Response<ProductDTO>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<ProductDTO>> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductBySlugAsync(request.Slug);
            if (product is null)
                return new Response<ProductDTO>(null, 404, ResponseMessages.PRODUCTS_RETRIEVAL_FAILED.GetDescription());

            var result = ProductDTO.MapFromEntity(product);

            return new Response<ProductDTO>(result, 200, ResponseMessages.PRODUCTS_RETRIEVED_SUCCESS.GetDescription());
        }
    }
}
