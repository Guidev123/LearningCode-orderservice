﻿using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Services;
using Orders.Domain.Request.Orders;
using Orders.Domain.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Orders
{
    public class CancelOrderEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
                => app.MapPost("/{id}/cancel", HandleAsync)
                    .WithOrder(2)
                    .Produces<Response<Order?>>();

        private static async Task<IResult> HandleAsync(
            IOrderService orderService,
            long id,
            ClaimsPrincipal user)
        {
            var request = new CancelOrderRequest
            {
                Id = id,
                UserId = user.Identity!.Name ?? string.Empty
            };

            var result = await orderService.CancelOrderAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}