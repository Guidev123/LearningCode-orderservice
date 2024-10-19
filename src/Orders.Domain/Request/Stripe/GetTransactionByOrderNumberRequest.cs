using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Request.Stripe
{
    public class GetTransactionByOrderNumberRequest
    {
        public GetTransactionByOrderNumberRequest(string number)
        {
            Number = number;
        }
        public string Number { get; private set; }
    }
}
