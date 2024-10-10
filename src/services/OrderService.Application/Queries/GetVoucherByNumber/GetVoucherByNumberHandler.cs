using MediatR;
using OrderService.Application.Responses;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries.GetVoucherByNumber
{
    public class GetVoucherByNumberHandler(IVoucherRepository voucherRepository) : IRequestHandler<GetVoucherByNumberQuery, Response<Voucher>>
    {
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        public async Task<Response<Voucher>> Handle(GetVoucherByNumberQuery request, CancellationToken cancellationToken)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(request.Number);

            if (voucher is null)
                return new Response<Voucher>(voucher, 404, "Erro: Voucher nao encontrado");

            return new Response<Voucher>(voucher, 200, "Sucesso: Voucher encontrado");
        }
    }
}
