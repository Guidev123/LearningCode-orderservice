namespace Orders.Domain.Request.Vouchers
{
    public class GetVoucherByNumberRequest
    {
        public GetVoucherByNumberRequest(string number)
        {
            Number = number;
        }

        public string Number { get; private set; } 
    }
}
