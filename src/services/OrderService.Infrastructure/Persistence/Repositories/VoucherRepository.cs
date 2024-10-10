using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class VoucherRepository(OrderDbContext context) : IVoucherRepository
    {
        private readonly OrderDbContext _context = context;
        public async Task<Voucher?> GetVoucherByIdAsync(string number) =>
            await _context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Number == number && x.IsActive);
    }
}
