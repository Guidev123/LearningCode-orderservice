using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<Response>
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
