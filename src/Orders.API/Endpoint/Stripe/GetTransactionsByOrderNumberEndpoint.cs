using MediatR;
using Orders.Application.Queries.GetTransactionByOrderNumber;
using Orders.Application.Response;
using System.Security.Claims;

namespace Orders.API.Endpoint.Stripe
{
    public class GetTransactionsByOrderNumberEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{number}/transactions", HandleAsync)
                .Produces<Response<dynamic>>();

        private static async Task<IResult> HandleAsync(IMediator mediator,
                                                       string number)
        {
            var result = await mediator.Send(new GetTransactionByOrderNumberQuery(number));
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
