using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<Response<Order>>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
