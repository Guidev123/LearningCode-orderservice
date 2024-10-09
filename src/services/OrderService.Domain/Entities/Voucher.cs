﻿namespace OrderService.Domain.Entities
{
    public class Voucher : Entity
    {
        public Voucher(string title, string description,
                       decimal amount, DateTime startDate, DateTime endDate)
        {
            Number = Guid.NewGuid().ToString("N")[..8];
            Title = title;
            Description = description;
            IsUsed = false;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Number { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsUsed { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsActive() => StartDate >= DateTime.Now && EndDate >= DateTime.Now && IsUsed is false;
        public void SetVoucherAsUsed() => IsUsed = true;
    }
}
