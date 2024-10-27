using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetVoucherByNumber
{
    public class GetVoucherByNumberHandler(IVoucherRepository voucherRepository) : IRequestHandler<GetVoucherByNumberQuery, Response<VoucherDTO?>>
    {
        private readonly IVoucherRepository _voucherRepository = voucherRepository;
        public async Task<Response<VoucherDTO?>> Handle(GetVoucherByNumberQuery request, CancellationToken cancellationToken)
        {
            var voucher = await _voucherRepository.GetVoucherByNumberAsync(request.Number);
            if (voucher is null)
                return new Response<VoucherDTO?>(null, 404, ResponseMessages.VOUCHER_NOT_FOUND.GetDescription());

            var result = VoucherDTO.MapFromEntity(voucher);

            return new Response<VoucherDTO?>(result, 200, ResponseMessages.VOUCHER_RETRIEVED_SUCCESS.GetDescription());
        }
    }
}
