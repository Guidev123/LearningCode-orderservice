using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(OrderDbContext context) : IVoucherRepository
    {
        private readonly OrderDbContext _context = context;

        public async Task<Voucher?> GetVoucherByIdAsync(Guid? voucherId) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == voucherId && x.IsActive);

        public async Task<Voucher?> GetVoucherByNumberAsync(string number) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Number == number && x.IsActive);

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
