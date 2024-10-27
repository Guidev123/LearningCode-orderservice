using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Queries.GetOrderByNumber
{
    public class GetOrderByNumberQuery : IRequest<Response<OrderDTO>>
    {
        public GetOrderByNumberQuery(string number, string userId)
        {
            Number = number;
            UserId = userId;
        }
        public string Number { get; private set; }
        public string UserId { get; private set; }
    }
}
