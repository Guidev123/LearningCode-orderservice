using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Middlewares;
using Orders.Application.DTOs;
using Orders.Application.Queries.GetAllOrders;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class GetAllOrdersEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithOrder(5)
                .Produces<PagedResponse<List<OrderDTO>?>>();

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user,
                                                       IMediator mediator,
                                                       [FromQuery] int pageNumber = ApplicationModule.DEFAULT_PAGE_NUMBER,
                                                       [FromQuery] int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new GetAllOrdersQuery(pageNumber, pageSize, userIdClaim!.Value));

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
