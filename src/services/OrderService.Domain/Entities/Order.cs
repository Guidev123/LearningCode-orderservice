using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Order : Entity
    {
        public Order(string number, Guid userId, string? externalReference, Guid? voucherId, Guid productId)
        {
            Number = number;
            UserId = userId;
            ExternalReference = externalReference;
            VoucherId = voucherId;
            ProductId = productId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            PaymentGateway = EPaymentGateway.Stripe;
            Status = EOrderStatus.WaitingPayment;
        }

        public string Number { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string? ExternalReference { get; private set; }
        public EPaymentGateway PaymentGateway { get; private set; }
        public EOrderStatus Status { get; private set; }
        public Guid? VoucherId { get; private set; }
        public Guid ProductId { get; private set; }
        public Voucher? Voucher { get; private set; }
        public Product Product { get; private set; } = null!;

        public decimal Total() => Product.Price - (Voucher?.Amount ?? 0);
    }
}
