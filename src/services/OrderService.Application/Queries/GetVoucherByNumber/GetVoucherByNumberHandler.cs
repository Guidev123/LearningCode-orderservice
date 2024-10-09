using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetVoucherByNumber
{
    public class GetVoucherByNumberHandler : IRequestHandler<GetVoucherByNumberQuery, Response>
    {
        public Task<Response> Handle(GetVoucherByNumberQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
