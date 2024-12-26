using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Configurations;
using Orders.Application.DTOs;
using Orders.Application.Queries.GetAllOrders;
using Orders.Application.Queries.GetAllProducts;
using Orders.Application.Response;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using System.Security.Claims;

namespace Orders.API.Endpoint.Products
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithOrder(1)
                .Produces<PagedResponse<List<ProductDTO>?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       ClaimsPrincipal user,
                                                       [FromQuery] int pageNumber = ApplicationModule.DEFAULT_PAGE_NUMBER,
                                                       [FromQuery] int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new GetAllProductsQuery(pageNumber, pageSize));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
