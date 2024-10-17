using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Response.Stripe
{
    public class StripeTransactionData
    {
        public StripeTransactionData(string id, string email, long amount, long amountCaptured, string status, bool paid, bool refunded)
        {
            Id = id;
            Email = email;
            Amount = amount;
            AmountCaptured = amountCaptured;
            Status = status;
            Paid = paid;
            Refunded = refunded;
        }

        public string Id { get; private set; } 
        public string Email { get; private set; }
        public long Amount { get; private set; }
        public long AmountCaptured { get; private set; }
        public string Status { get; private set; }
        public bool Paid { get; private set; }
        public bool Refunded { get; private set; }
    }
}
