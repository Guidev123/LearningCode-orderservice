using Orders.Domain.Entities;
using Orders.Domain.Request.Vouchers;
using Orders.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Interfaces.Repositories
{
    public interface IVoucherRepository
    {
        Task<Response<Voucher?>> GetVoucherByNumberAsync(GetVoucherByNumberRequest request);
        Task<Voucher?> GetVoucherByIdAsync(long? voucherId);
        Task UpdateVoucherAsync(Voucher voucher);
    }
}
