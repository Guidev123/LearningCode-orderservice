using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Commands.RefundOrder
{
    public class RefundOrderCommand : IRequest<Response<OrderDTO?>>
    {
        public RefundOrderCommand(long id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public long Id { get; private set; }
        public string UserId { get; private set; }
    }
}
