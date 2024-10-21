using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class PayOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/{number}/pay", HandleAsync)
                .WithOrder(3)
                .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(IOrderService orderService,
                                                       string number,
                                                       PayOrderRequest request,
                                                       ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var order = new PayOrderRequest(userIdClaim?.Value ?? string.Empty, number, request.OrderId);

            var result = await orderService.PayOrderAsync(order);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
