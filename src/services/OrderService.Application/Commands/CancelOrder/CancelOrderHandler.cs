using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, Response<Order>>
    {
        public Task<Response<Order>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
