using MediatR;
using OrderService.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetVoucherByNumber
{
    public class GetVoucherByNumberQuery : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}
