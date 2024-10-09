using MediatR;
using OrderService.Application.Responses;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Response>
    {
        public Guid UserId { get; set; }
    }
}
