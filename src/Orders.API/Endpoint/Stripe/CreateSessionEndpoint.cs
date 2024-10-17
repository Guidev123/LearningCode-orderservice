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

            request.UserId = userIdClaim?.Value ?? string.Empty;
            request.UserEmail = userEmailClaim?.Value ?? string.Empty;


            var result = await stripeService.CreateSessionAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
