using Microsoft.AspNetCore.Mvc;
using Orders.Domain.Interfaces.ExternalServices;
using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Stripe
{
    public class GetTransactionsByOrderNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}/transactions", HandleAsync)
                .Produces<Response<dynamic>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       IStripeService stripeService,
                                                       string number)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var request = new GetTransactionByOrderNumberRequest
            {
                UserId = userIdClaim?.Value ?? string.Empty,
                Number = number
            };

            var result = await stripeService.GetTransactionsByOrderNumberAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
