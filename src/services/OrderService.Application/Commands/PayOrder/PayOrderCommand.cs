using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.PayOrder
{
    public class PayOrderCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
    }
}
