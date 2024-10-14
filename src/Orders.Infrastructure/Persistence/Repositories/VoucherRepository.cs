using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Vouchers;
using Orders.Domain.Response;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(OrdersDbContext context) : IVoucherRepository
    {
        private readonly OrdersDbContext _context = context;

        public async Task<Voucher?> GetVoucherByIdAsync(long? voucherId) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == voucherId && x.IsActive());

        public async Task<Response<Voucher?>> GetVoucherByNumberAsync(GetVoucherByNumberRequest request) =>
             new Response<Voucher?>(await _context.Vouchers.AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.Number == request.Number && x.IsActive()));

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
