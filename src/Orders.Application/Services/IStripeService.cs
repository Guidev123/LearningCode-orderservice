using Orders.Application.DTOs;

namespace Orders.Application.Services
{
    public interface IStripeService
    {
        Task<string?> CreateSessionAsync(CreateSessionDTO command);
        Task<List<StripeTransactionDTO>> GetTransactionsByOrderNumberAsync(string number);
    }
}
