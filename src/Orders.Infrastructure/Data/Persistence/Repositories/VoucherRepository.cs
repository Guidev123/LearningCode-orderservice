using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Data.Persistence.Repositories
{
    public class VoucherRepository(OrdersDbContext context) : IVoucherRepository
    {
        private readonly OrdersDbContext _context = context;

        public async Task<Voucher?> GetVoucherByIdAsync(long? voucherId) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == voucherId && !x.IsUsed);

        public async Task<Voucher?> GetVoucherByNumberAsync(string number) => await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Number == number && !x.IsUsed);

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
