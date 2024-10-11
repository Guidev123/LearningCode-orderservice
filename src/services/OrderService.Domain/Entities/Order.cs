using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities
{
    public class Order : Entity
    {
        public Order(Guid userId, Guid? voucherId, Guid productId, Product product, Voucher? voucher = null, string? number = null,
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

        public string? Number { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string? ExternalReference { get; private set; }
        public EPaymentGateway PaymentGateway { get; private set; }
        public EOrderStatus Status { get; private set; }
        public Guid? VoucherId { get; private set; }
        public Guid ProductId { get; private set; }
        public Voucher? Voucher { get; private set; }
        public Product Product { get; private set; }
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
