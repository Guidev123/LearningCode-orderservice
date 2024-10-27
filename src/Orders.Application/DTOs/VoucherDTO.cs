using Orders.Domain.Entities;

namespace Orders.Application.DTOs
{
    public class VoucherDTO
    {
        public VoucherDTO(string number, string title, string description, decimal amount, DateTime startDate, DateTime endDate)
        {
            Number = number;
            Title = title;
            Description = description;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Number { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public static VoucherDTO MapFromEntity(Voucher voucher) =>
            new(voucher.Number, voucher.Title, voucher.Description, voucher.Amount, voucher.StartDate, voucher.EndDate);
    }
}
