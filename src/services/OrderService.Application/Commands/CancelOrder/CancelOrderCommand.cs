using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<Response<Order>>
    {
        public string Number { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
