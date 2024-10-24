using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.MessageBus.Messages
{
    public class UpdateUserRoleMessage
    {
        public UpdateUserRoleMessage(Guid userId, bool isPaid)
        {
            UserId = userId;
            IsPaid = isPaid;
        }

        public Guid UserId { get; private set; }
        public bool IsPaid { get; private set; }
    }
}
