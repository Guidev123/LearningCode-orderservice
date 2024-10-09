using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VoucherId { get; set; }
    }
}
