using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Commands.PayOrder
{
    public class PayOrderCommand : IRequest<Response<OrderDTO?>>
    {
        public PayOrderCommand(string userId, string orderNumber)
        {
            OrderNumber = orderNumber;
            UserId = userId;
        }

        public string OrderNumber { get; private set; }
        public string UserId { get; private set; }
    }
}
