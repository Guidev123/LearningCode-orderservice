using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Response<Order>>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VoucherId { get; set; }
    }
}
