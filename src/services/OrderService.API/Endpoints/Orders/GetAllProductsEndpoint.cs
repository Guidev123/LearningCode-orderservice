using MediatR;
using OrderService.Application.Queries.GetAllOrders;
using OrderService.Application.Queries.GetAllProducts;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.Orders
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
                app.MapGet("/products", HandleAsync)
                .Produces<PagedResponse<List<Product>?>>();


        private static async Task<IResult> HandleAsync(IMediator mediator, GetAllProductsQuery query)
        {
            var result = await mediator.Send(query);

            if (!result.IsSuccess)
                return TypedResults.BadRequest(result.Message);

            return TypedResults.Ok(result);
        }
    }
}
