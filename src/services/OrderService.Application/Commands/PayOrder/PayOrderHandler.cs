using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.PayOrder
{
    public class PayOrderHandler : IRequestHandler<PayOrderCommand, Response>
    {
        public Task<Response> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
