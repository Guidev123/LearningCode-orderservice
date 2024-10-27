using MediatR;
using Orders.Application.Commands.CancelOrder;
using Orders.Application.DTOs;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class CancelOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/{id}/cancel", HandleAsync)
                    .WithOrder(2)
                    .Produces<Response<OrderDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       long id,
                                                       ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new CancelOrderCommand(id, userIdClaim!.Value));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
