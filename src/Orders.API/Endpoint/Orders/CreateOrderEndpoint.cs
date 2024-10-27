using MediatR;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.DTOs;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class CreateOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithOrder(1)
                .Produces<Response<OrderDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       CreateOrderCommand command,
                                                       ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new CreateOrderCommand(command.ProductId, userIdClaim!.Value, command.VoucherId));

            return result.IsSuccess
                ? TypedResults.Created($"api/orders/{result.Data?.Number}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
