using MediatR;
using OrderService.Application.Queries.GetAllOrders;
using OrderService.Application.Responses;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Response>
    {
        public Task<Response> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
