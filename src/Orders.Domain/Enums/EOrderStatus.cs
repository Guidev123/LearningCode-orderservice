using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Enums
{
    public enum EOrderStatus
    {
        WaitingPayment = 1,
        Paid = 2,
        Canceled = 3,
        Refunded = 4
    }
}
