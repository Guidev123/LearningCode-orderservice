using MediatR;
using Orders.Application.Commands.CreateSession;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Stripe
{
    public class CreateSessionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/session", HandleAsync)
                .Produces<Response<string?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       CreateSessionCommand command,
                                                       IMediator mediator)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userEmailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            var result = await mediator.Send(new CreateSessionCommand(userEmailClaim!.Value, userIdClaim!.Value, command.OrderNumber,
                                                                      command.ProductTitle, command.ProductDescription, command.OrderTotal));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
