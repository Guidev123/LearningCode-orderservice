using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetProductBySlug
{
    public class GetProductBySlugQuery : IRequest<Response<Product>>
    {
        public string Slug { get; set; } = string.Empty;
    }
}
