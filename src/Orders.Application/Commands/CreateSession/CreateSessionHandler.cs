using MediatR;
using Orders.Application.Response;
using Orders.Application.Response.Messages;
using Orders.Application.Services;

namespace Orders.Application.Commands.CreateSession
{
    public class CreateSessionHandler(IStripeService stripeService) : IRequestHandler<CreateSessionCommand, Response<string?>>
    {
        private readonly IStripeService _stripeService = stripeService;
        public async Task<Response<string?>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _stripeService.CreateSessionAsync(new(request.UserEmail, request.UserId, request.OrderNumber,
                                                                      request.ProductTitle, request.ProductDescription, request.OrderTotal));
            if (session is null)
                return new Response<string?>(null, 404, ResponseMessages.PAYMENT_FAILED.GetDescription());

            return new Response<string?>(session, 200, ResponseMessages.SESSION_CREATED.GetDescription());
        }
    }
}
