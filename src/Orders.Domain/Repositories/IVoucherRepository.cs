using Orders.Domain.Entities;

namespace Orders.Domain.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetVoucherByNumberAsync(string number);
        Task<Voucher?> GetVoucherByIdAsync(long? voucherId);
        Task UpdateVoucherAsync(Voucher voucher);
    }
}
