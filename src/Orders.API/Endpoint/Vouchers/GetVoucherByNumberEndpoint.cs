using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Queries.GetVoucherByNumber;
using Orders.Application.Response;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.API.Endpoint.Vouchers
{
    public class GetVoucherByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}", HandleAsync)
                .WithOrder(4)
                .Produces<Response<VoucherDTO?>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       string number)
        {
            var result = await mediator.Send(new GetVoucherByNumberQuery(number));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
