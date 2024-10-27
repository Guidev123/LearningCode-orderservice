using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Domain.Entities;
using System.Text.Json.Serialization;

namespace Orders.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Response<OrderDTO?>>
    {
        public CreateOrderCommand(long productId, string userId, long? voucherId = null)
        {
            ProductId = productId;
            VoucherId = voucherId;
            UserId = userId;
        }

        public long ProductId { get; private set; }
        public long? VoucherId { get; private set; }
        [JsonIgnore]
        public string UserId { get; private set; }
    }
}
