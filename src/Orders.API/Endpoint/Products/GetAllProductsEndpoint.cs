using Microsoft.AspNetCore.Mvc;
using Orders.API.Middlewares;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Products;
using Orders.Domain.Response;

namespace Orders.API.Endpoint.Products
{
    public class GetAllProductsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithOrder(1)
                .Produces<PagedResponse<List<Product>?>>();

        private static async Task<IResult> HandleAsync(
            IProductRepository productRepository,
            [FromQuery] int pageNumber = ApplicationModule.DEFAULT_PAGE_NUMBER,
            [FromQuery] int pageSize = ApplicationModule.DEFAULT_PAGE_SIZE)
        {
            var request = new GetAllProductsRequest(pageNumber, pageSize);

            var result = await productRepository.GetAllProductsAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result.Data);
        }
    }
}
