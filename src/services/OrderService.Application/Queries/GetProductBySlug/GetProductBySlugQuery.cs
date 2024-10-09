using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugQuery : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public string Slug { get; set; } = string.Empty;
    }
}
