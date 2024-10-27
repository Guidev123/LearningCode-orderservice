using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Queries.GetProductBySlug;
using Orders.Application.Response;
using Orders.Domain.Repositories;

namespace Orders.API.Endpoint.Products
{
    public class GetProductBySlugEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{slug}", HandleAsync)
                .WithOrder(4)
                .Produces<Response<ProductDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       string slug)
        {

            var result = await mediator.Send(new GetProductBySlugQuery(slug));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
