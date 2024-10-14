namespace Orders.Domain.Request.Vouchers
{
    public class GetVoucherByNumberRequest : Request
    {
        public string Number { get; set; } = string.Empty;
    }
}
