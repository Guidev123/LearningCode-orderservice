using MediatR;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Queries.GetOrderByNumber;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/{number}", HandleAsync)
            .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator, GetOrderByNumberQuery command)
        {
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
                return TypedResults.BadRequest(result.Message);

            return TypedResults.Ok(result);
        }

    }
}
