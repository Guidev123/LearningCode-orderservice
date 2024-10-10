using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetOrderByNumber
{
    public class GetOrderByNumberHandler : IRequestHandler<GetOrderByNumberQuery, Response<Order>>
    {
        public Task<Response<Order>> Handle(GetOrderByNumberQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
