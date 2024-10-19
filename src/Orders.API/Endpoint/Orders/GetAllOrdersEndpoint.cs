using Microsoft.AspNetCore.Mvc;
using Orders.API.Middlewares;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithOrder(5)
                .Produces<PagedResponse<List<Order>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IOrderRepository orderRepository,
            [FromQuery] int pageNumber = ApplicationModule.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var request = new GetAllOrdersRequest(userIdClaim?.Value ?? string.Empty, pageNumber, pageSize);

            var result = await orderRepository.GetAllOrdersAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
