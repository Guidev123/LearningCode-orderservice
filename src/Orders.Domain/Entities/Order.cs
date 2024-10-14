using Orders.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Entities
{
    public class Order
    {
        public Order(string userId,
                      long productId, Product product,
                      Voucher? voucher = null,
                      long? voucherId = null, string? number = null,
                      string? externalReference = null)
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
            Product = product;
            Voucher = voucher;
        }
        protected Order() { }

        public long Id { get; set; }
        public string? Number { get; private set; }
        public string UserId { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string? ExternalReference { get; private set; }
        public EPaymentGateway PaymentGateway { get; private set; }
        public EOrderStatus Status { get; private set; }
        public long? VoucherId { get; private set; }
        public long ProductId { get; private set; }
        public Voucher? Voucher { get; private set; }
        public Product Product { get; private set; } = null!;
        public decimal Total() => Product.Price - (Voucher?.Amount ?? 0);
        public void CancellStatusOrder()
        {
            Status = EOrderStatus.Canceled;
            UpdatedAt = DateTime.Now;
        }
        public void RefundStatusOrder()
        {
            Status = EOrderStatus.Refunded;
            UpdatedAt = DateTime.Now;
        }
        public void PayStatusOrder(string externalReference)
        {
            Status = EOrderStatus.Paid;
            ExternalReference = externalReference;
            UpdatedAt = DateTime.Now;
        }
    }
}
