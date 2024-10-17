using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;
using Orders.Domain.Response.Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces.ExternalServices
{
    public interface IStripeService
    {
        Task<Response<StripeSessionData>> CreateSessionAsync(CreateSessionRequest request);
        Task<Response<List<StripeTransactionData>>> GetTransactionsByOrderNumberAsync(GetTransactionByOrderNumberRequest request);
    }
}
