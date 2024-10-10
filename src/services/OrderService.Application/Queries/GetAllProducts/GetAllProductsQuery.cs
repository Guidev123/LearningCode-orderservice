using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Response<Product>>
    {
    }
}
