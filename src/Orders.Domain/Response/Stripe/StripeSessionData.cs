using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Response.Stripe
{
    public class StripeSessionData
    {
        public StripeSessionData(string sessionId, string checkUrl)
        {
            SessionId = sessionId;
            CheckUrl = checkUrl;
        }

        public string SessionId { get; private set; }
        public string CheckUrl { get; private set; }
    }
}
