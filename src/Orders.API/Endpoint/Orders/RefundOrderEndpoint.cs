using MediatR;
using Orders.Application.Commands.RefundOrder;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Domain.Entities;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class RefundOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/{id}/refund", HandleAsync)
                .WithOrder(4)
                .Produces<Response<OrderDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       long id,
                                                       ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new RefundOrderCommand(id, userIdClaim!.Value));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
