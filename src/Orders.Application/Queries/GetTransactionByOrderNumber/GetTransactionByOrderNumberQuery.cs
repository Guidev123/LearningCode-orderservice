using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Response;

namespace Orders.Application.Queries.GetTransactionByOrderNumber
{
    public class GetTransactionByOrderNumberQuery : IRequest<Response<List<StripeTransactionDTO>?>>
    {
        public GetTransactionByOrderNumberQuery(string number) => Number = number;
        public string Number { get; private set; }

    }
}
