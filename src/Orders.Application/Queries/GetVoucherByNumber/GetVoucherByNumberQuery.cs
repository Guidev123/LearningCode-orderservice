using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Queries.GetVoucherByNumber
{
    public class GetVoucherByNumberQuery : IRequest<Response<VoucherDTO?>>
    {
        public GetVoucherByNumberQuery(string number) => Number = number;
        public string Number { get; private set; }
    }
}
