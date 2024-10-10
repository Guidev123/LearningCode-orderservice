using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.PayOrder
{
    public class PayOrderHandler : IRequestHandler<PayOrderCommand, Response<Order>>
    {
        public Task<Response<Order>> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
