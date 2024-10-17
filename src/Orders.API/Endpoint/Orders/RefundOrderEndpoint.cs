using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class RefundOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/{id}/refund", HandleAsync)
                .WithOrder(4)
                .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(
            IOrderService orderService,
            long id,
            ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var request = new RefundOrderRequest()
            {
                Id = id,
                UserId = userIdClaim?.Value ?? string.Empty
            };

            var result = await orderService.RefundOrderAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
