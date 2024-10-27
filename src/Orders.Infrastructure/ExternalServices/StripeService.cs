using Azure;
using Microsoft.Extensions.Options;
using Orders.Infrastructure.DTOs;
using Stripe;
using Stripe.Checkout;

namespace Orders.Infrastructure.ExternalServices
{
    public class StripeService(IOptions<Configuration.StripeConfiguration> stripeSettings) : IStripeService
    {
        private readonly Configuration.StripeConfiguration _stripeSettings = stripeSettings.Value;

        public async Task<string?> CreateSessionAsync(CreateSessionDTO request)
        {
            var client = new StripeClient(_stripeSettings.ApiKey);

            var options = new SessionCreateOptions
            {
                CustomerEmail = request.UserEmail,
                ClientReferenceId = request.UserId,

                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                        {
                            { "order", request.OrderNumber }
                        }
                },
                PaymentMethodTypes = [_stripeSettings.PaymentMethodTypes],
                LineItems =
                [
                    new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "BRL",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = request.ProductTitle,
                                    Description = request.ProductDescription
                                },
                                UnitAmount = request.OrderTotal,
                            },
                            Quantity = 1
                        }
                ],
                Mode = _stripeSettings.StripeMode,
                SuccessUrl = $"{_stripeSettings.FrontendUrl}/orders/{request.OrderNumber}/confirm",
                CancelUrl = $"{_stripeSettings.FrontendUrl}/orders/{request.OrderNumber}/cancel",
            };

            var service = new SessionService(client);
            var session = await service.CreateAsync(options);

            return session.Id;

        }

        public async Task<List<StripeTransactionDTO>> GetTransactionsByOrderNumberAsync(string number)
        {
            var client = new StripeClient(_stripeSettings.ApiKey);

            var options = new ChargeSearchOptions
            {
                Query = $"metadata['order']:'{number}'",
            };

            var service = new ChargeService(client);
            var result = await service.SearchAsync(options);

            if (result.Data.Count == 0)
                return [];

            var data = new List<StripeTransactionDTO>();
            foreach (var item in result.Data)
            {
                data.Add(new StripeTransactionDTO(item.Id, item.BillingDetails.Email, item.Amount,
                                                   item.AmountCaptured, item.Status, item.Paid, item.Refunded));
            }

            return data;
        }
    }
}
