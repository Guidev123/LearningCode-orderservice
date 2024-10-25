using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;

namespace Orders.Domain.Interfaces.ExternalServices
{
    public interface IStripeService
    {
        Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request);
        Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionByOrderNumberRequest request);
    }
}
