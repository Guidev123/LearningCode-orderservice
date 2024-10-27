using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugQuery : IRequest<Response<ProductDTO>>
    {
        public string Slug { get; private set; }

        public GetProductBySlugQuery(string slug) => Slug = slug;
    }
}
