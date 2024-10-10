using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugHandler(IProductRepository productRepository) : IRequestHandler<GetProductBySlugQuery, Response<Product>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<Response<Product>> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductBySlugAsync(request.Slug);

            if (product is null)
                return new Response<Product>(product, 404, "Erro: Produto nao encontrado");

            return new Response<Product>(product, 200, "Sucesso: Produto encontrado");
        }
    }
}
