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
    public class GetOrderByNumberQuery : IRequest<Response<Order>>
    {
        public string UserId { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
    }
}
