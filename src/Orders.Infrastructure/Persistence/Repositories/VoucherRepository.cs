using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces.Repositories;
using Orders.Domain.Request.Vouchers;
using Orders.Domain.Response;
using Orders.Domain.Response.Messages;

namespace Orders.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(OrdersDbContext context) : IVoucherRepository
    {
        private readonly OrdersDbContext _context = context;

        public async Task<Voucher?> GetVoucherByIdAsync(long? voucherId) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == voucherId && !x.IsUsed);

        public async Task<Response<Voucher?>> GetVoucherByNumberAsync(GetVoucherByNumberRequest request)
        {

             var voucher = await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Number == request.Number && !x.IsUsed);
            if(voucher is null)
                return new Response<Voucher?>(null, 404, ResponseMessages.VOUCHER_NOT_FOUND.GetDescription());

            return new Response<Voucher?>(voucher, 200, ResponseMessages.VOUCHER_RETRIEVED_SUCCESS.GetDescription());
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}
