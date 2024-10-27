using MediatR;
using Orders.Application.Commands.PayOrder;
using Orders.Application.DTOs;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class PayOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/{number}/pay", HandleAsync)
                .WithOrder(3)
                .Produces<Response<OrderDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       string number,
                                                       ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new PayOrderCommand(userIdClaim!.Value, number));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
