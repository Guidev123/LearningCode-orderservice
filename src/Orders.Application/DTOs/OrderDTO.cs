using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Application.DTOs
{
    public class OrderDTO
    {
        public OrderDTO(string? number, DateTime createdAt, DateTime updatedAt, string status, Voucher? voucher, Product product, decimal total)
        {
            Number = number;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
            Voucher = voucher;
            Product = product;
            Total = total;
        }

        public string? Number { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string Status { get; private set; }
        public Voucher? Voucher { get; private set; }
        public Product Product { get; private set; } = null!;
        public decimal Total { get; private set; }

        public static OrderDTO MapFromEntity(Order order) =>
            new(order.Number, order.CreatedAt, order.UpdatedAt, order.Status.ToString(),
                order.Voucher, order.Product, order.Total);
    }
}
