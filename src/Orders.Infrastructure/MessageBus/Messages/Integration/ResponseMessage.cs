using Orders.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.MessageBus.Messages.Integration
{
    public class ResponseMessage<TData>
    {
        public Response<TData> Response { get; private set; } = null!;

        public ResponseMessage(Response<TData> response)
        {
            Response = response;
        }
    }
}
