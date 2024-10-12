
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Queries.GetAllOrders;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.Orders
{
    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/", HandleAsync)
            .Produces<PagedResponse<List<Order>?>>();


        private static async Task<IResult> HandleAsync(IMediator mediator, GetAllOrdersQuery query)
        {
            var result = await mediator.Send(query);

            if (!result.IsSuccess)
                return TypedResults.BadRequest(result.Message);

            return TypedResults.Ok(result);
        }
    }
}
