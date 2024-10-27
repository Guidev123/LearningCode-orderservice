using Orders.Infrastructure.DTOs;

namespace Orders.Infrastructure.ExternalServices
{
    public interface IStripeService
    {
        Task<string?> CreateSessionAsync(CreateSessionDTO command);
        Task<List<StripeTransactionDTO>> GetTransactionsByOrderNumberAsync(string number);
    }
}
