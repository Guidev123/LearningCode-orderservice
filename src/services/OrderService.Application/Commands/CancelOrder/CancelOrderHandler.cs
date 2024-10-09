using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, Response>
    {
        public Task<Response> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
