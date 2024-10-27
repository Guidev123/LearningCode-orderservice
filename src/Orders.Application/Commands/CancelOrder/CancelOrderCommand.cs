using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<Response<OrderDTO?>>
    {
        public CancelOrderCommand(long id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public long Id { get; private set; }
        public string UserId { get; private set; }
    }
}
