using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class CreateOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithOrder(1)
                .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(
            IOrderService orderService,
            CreateOrderRequest request,
            ClaimsPrincipal user)
        {
            request.UserId = user.Identity!.Name ?? string.Empty;

            var result = await orderService.CreateOrderAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"api/orders/{result.Data?.Number}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
