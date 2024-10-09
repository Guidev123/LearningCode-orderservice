using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugHandler : IRequestHandler<GetProductBySlugQuery, Response>
    {
        public Task<Response> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
