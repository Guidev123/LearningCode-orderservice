using Orders.API.Endpoint.Orders;
using Orders.API.Endpoint.Products;
using Orders.API.Endpoint.Stripe;
using Orders.API.Endpoint.Vouchers;
using Orders.Domain.Request.Stripe;

namespace Orders.API.Endpoint
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("api/orders")
                .WithTags("Orders")
                .RequireAuthorization()
                .MapEndpoint<CancelOrderEndpoint>()
                .MapEndpoint<CreateOrderEndpoint>()
                .MapEndpoint<GetAllOrdersEndpoint>()
                .MapEndpoint<GetOrderByNumberEndpoint>()
                .MapEndpoint<PayOrderEndpoint>()
                .MapEndpoint<RefundOrderEndpoint>();

            endpoints.MapGroup("api/vouchers")
               .WithTags("Vouchers")
               .RequireAuthorization()
               .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoints.MapGroup("api/products")
               .WithTags("Products")
               .RequireAuthorization()
               .MapEndpoint<GetAllProductsEndpoint>()
               .MapEndpoint<GetProductBySlugEndpoint>();

            endpoints.MapGroup("api/payments")
               .WithTags("Payments")
               .RequireAuthorization()
               .MapEndpoint<GetTransactionsByOrderNumberEndpoint>()
               .MapEndpoint<CreateSessionEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
