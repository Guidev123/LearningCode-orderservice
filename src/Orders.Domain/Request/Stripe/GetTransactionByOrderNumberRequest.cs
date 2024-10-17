using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Request.Stripe
{
    public class GetTransactionByOrderNumberRequest : Request
    {
        public string Number { get; set; } = string.Empty;
    }
}
