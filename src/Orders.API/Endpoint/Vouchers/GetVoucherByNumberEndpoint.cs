using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Vouchers;
using Orders.Domain.Response;

namespace Orders.API.Endpoint.Vouchers
{
    public class GetVoucherByNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}", HandleAsync)
                .WithOrder(4)
                .Produces<Response<Voucher?>>();

        private static async Task<IResult> HandleAsync(
            IVoucherRepository voucherRepository,
            string number)
        {
            var request = new GetVoucherByNumberRequest(number);

            var result = await voucherRepository.GetVoucherByNumberAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
