using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.RefundOrder
{
    public class RefundOrderHandler : IRequestHandler<RefundOrderCommand, Response>
    {
        public Task<Response> Handle(RefundOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
