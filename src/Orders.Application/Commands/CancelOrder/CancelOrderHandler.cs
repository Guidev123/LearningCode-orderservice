using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Enums;
using Orders.Domain.Repositories;

namespace Orders.Application.Commands.CancelOrder
{
    public class CancelOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CancelOrderCommand, Response<OrderDTO?>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        public async Task<Response<OrderDTO?>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id, request.UserId);
            if (order is null)
                return new Response<OrderDTO?>(null, 404, ResponseMessages.ORDER_NOT_FOUND.GetDescription());

            switch (order.Status)
            {
                case EOrderStatus.WaitingPayment:
                    break;
                case EOrderStatus.Canceled:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_CANCELED.GetDescription());
                case EOrderStatus.Refunded:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_REFUNDED.GetDescription());
                case EOrderStatus.Paid:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_ALREADY_PAID.GetDescription());
                default:
                    return new Response<OrderDTO?>(null, 400, ResponseMessages.ORDER_CANNOT_BE_CANCELED.GetDescription());
            }

            order.CancellStatusOrder();
            await _orderRepository.UpdateOrderAsync(order);
            var result = OrderDTO.MapFromEntity(order);

            return new Response<OrderDTO?>(result, 200, ResponseMessages.ORDER_CANCELED_SUCCESS.GetDescription());
        }
    }
}
