using Microsoft.AspNetCore.Mvc;
using Orders.Domain.Interfaces.ExternalServices;
using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Stripe
{
    public class CreateSessionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/session", HandleAsync)
                .Produces<Response<string?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       IStripeService stripeService,
                                                       CreateSessionRequest request)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userEmailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            var session = new CreateSessionRequest(userEmailClaim?.Value ?? string.Empty, userIdClaim?.Value ?? string.Empty,
                                                   request.OrderNumber, request.ProductTitle, request.ProductDescription,
                                                   request.OrderTotal);

            var result = await stripeService.CreateSessionAsync(session);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
