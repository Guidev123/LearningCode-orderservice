using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Response<Order?>>
    {
        public string UserId { get; set; } = string.Empty;
        public Guid? VoucherId { get; set; }
        public Guid ProductId { get; set; } 
    }
}
