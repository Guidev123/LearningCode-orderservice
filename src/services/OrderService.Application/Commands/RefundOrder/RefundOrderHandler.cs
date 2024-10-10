using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.RefundOrder
{
    public class RefundOrderHandler : IRequestHandler<RefundOrderCommand, Response<Order>>
    {
        public Task<Response<Order>> Handle(RefundOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
