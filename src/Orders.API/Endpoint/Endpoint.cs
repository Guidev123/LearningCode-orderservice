using Orders.API.Endpoint.Orders;
using Orders.API.Endpoint.Products;
using Orders.API.Endpoint.Stripe;
using Orders.API.Endpoint.Vouchers;

namespace Orders.API.Endpoint
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("api/v1/orders")
                .WithTags("Orders")
                .RequireAuthorization()
                .MapEndpoint<CancelOrderEndpoint>()
                .MapEndpoint<CreateOrderEndpoint>()
                .MapEndpoint<GetAllOrdersEndpoint>()
                .MapEndpoint<GetOrderByNumberEndpoint>()
                .MapEndpoint<PayOrderEndpoint>()
                .MapEndpoint<RefundOrderEndpoint>();

            endpoints.MapGroup("api/v1/vouchers")
               .WithTags("Vouchers")
               .RequireAuthorization()
               .MapEndpoint<GetVoucherByNumberEndpoint>();

            endpoints.MapGroup("api/v1/products")
               .WithTags("Products")
               .RequireAuthorization()
               .MapEndpoint<GetAllProductsEndpoint>()
               .MapEndpoint<GetProductBySlugEndpoint>();

            endpoints.MapGroup("api/v1/payments")
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
