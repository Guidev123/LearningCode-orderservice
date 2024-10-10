﻿using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands.RefundOrder
{
    public class RefundOrderCommand : IRequest<Response<Order>>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
