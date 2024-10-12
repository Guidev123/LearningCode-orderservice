using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.RefundOrder
{
    public class RefundOrderCommand : IRequest<Response<Order>>
    {
        public string UserId { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
    }
}
