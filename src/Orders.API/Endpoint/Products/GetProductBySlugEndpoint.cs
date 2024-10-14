using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Products;
using Orders.Domain.Response;

namespace Orders.API.Endpoint.Products
{
    public class GetProductBySlugEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{slug}", HandleAsync)
                .WithOrder(4)
                .Produces<Response<Product?>>();

        private static async Task<IResult> HandleAsync(
            IProductRepository productRepository,
            string slug)
        {
            var request = new GetProductBySlugRequest
            {
                Slug = slug
            };

            var result = await productRepository.GetProductBySlugAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
