using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Queries.GetAllOrders
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, Response>
    {
        public Task<Response> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
