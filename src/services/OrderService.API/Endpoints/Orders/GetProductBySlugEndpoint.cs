
using MediatR;
using OrderService.Application.Queries.GetProductBySlug;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.Orders
{
    public class GetProductBySlugEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/{slug}", HandleAsync)
            .Produces<PagedResponse<List<Order>?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator, GetProductBySlugQuery query)
        {
            var result = await mediator.Send(query);

            if (!result.IsSuccess)
                return TypedResults.BadRequest(result.Message);

            return TypedResults.Ok(result);
        }
    }
}
