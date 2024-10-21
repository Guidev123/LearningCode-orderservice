using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class GetOrderByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}", HandleAsync)
                .WithOrder(6)
                .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       IOrderRepository orderRepository,
                                                       string number)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var request = new GetOrderByNumberRequest(number, userIdClaim?.Value ?? string.Empty);

            var result = await orderRepository.GetOrderByNumberAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
