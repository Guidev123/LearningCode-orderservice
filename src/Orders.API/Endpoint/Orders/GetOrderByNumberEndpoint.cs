using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Queries.GetOrderByNumber;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}", HandleAsync)
                .WithOrder(6)
                .Produces<Response<OrderDTO?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       IMediator mediator,
                                                       string number)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new GetOrderByNumberQuery(number, userIdClaim!.Value));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
