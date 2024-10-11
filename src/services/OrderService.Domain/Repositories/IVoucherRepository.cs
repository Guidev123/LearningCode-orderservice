using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Repositories
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetVoucherByNumberAsync(string number);
        Task<Voucher?> GetVoucherByIdAsync(Guid? voucherId);
        Task UpdateVoucherAsync(Voucher voucher);
    }
}
