using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Application.Services;

namespace Orders.Application.Queries.GetTransactionByOrderNumber
{
    public class GetTransactionByOrderNumberHandler(IStripeService stripeService)
               : IRequestHandler<GetTransactionByOrderNumberQuery, Response<List<StripeTransactionDTO>?>>
    {
        private readonly IStripeService _stripeService = stripeService;
        public async Task<Response<List<StripeTransactionDTO>?>> Handle(GetTransactionByOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _stripeService.GetTransactionsByOrderNumberAsync(request.Number);

            if (result is null)
                return new Response<List<StripeTransactionDTO>?>(null, 404, ResponseMessages.PAYMENT_NOT_FOUND.GetDescription());

            return new Response<List<StripeTransactionDTO>?>(result, 200, ResponseMessages.TRANSACTION_RETRIEVED_SUCCESS.GetDescription());
        }
    }
}
