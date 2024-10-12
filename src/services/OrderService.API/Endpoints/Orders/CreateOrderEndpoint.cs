
using MediatR;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.API.Endpoints.Orders
{
    public class CreateOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapPost("/create", HandleAsync)
            .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator, CreateOrderCommand command)
        {
            var result = await mediator.Send(command);

            if (!result.IsSuccess)
                return TypedResults.BadRequest(result.Message);

            return TypedResults.Created($"api/orders/{result.Data?.Number}" ,result);
        }
    }
}
