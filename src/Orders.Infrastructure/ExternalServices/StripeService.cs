﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Orders.Domain.Interfaces.ExternalServices;
using Orders.Domain.Request.Stripe;
using Orders.Domain.Response;
using Orders.Domain.Response.Stripe;
using Orders.Infrastructure.Models;
using Stripe;
using Stripe.Checkout;

namespace Orders.Infrastructure.ExternalServices
{
    public class StripeService : IStripeService
    {
        private readonly StripeConfigurationSettings _stripeSettings;
        public StripeService(IOptions<StripeConfigurationSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<Response<StripeSessionData>> CreateSessionAsync(CreateSessionRequest request)
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
                PaymentMethodTypes = ["card"],
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
                Mode = "payment",
                SuccessUrl = $"https://localhost:8080/pedidos/{request.OrderNumber}/confirmar",
                CancelUrl = $"https://localhost:8080/pedidos/{request.OrderNumber}/cancelar",
            };

            var service = new SessionService(client);
            var session = await service.CreateAsync(options);
            var checkoutUrl = session.Url;

            var result = new StripeSessionData(session.Id, checkoutUrl);

            return new Response<StripeSessionData>(result, 200, "Sucess");
        }

        public async Task<Response<List<StripeTransactionData>>> GetTransactionsByOrderNumberAsync(GetTransactionByOrderNumberRequest request)
        {
            var options = new ChargeSearchOptions
            {
                Query = $"metadata['order']:'{request.Number}'",
            };

            var service = new ChargeService();
            var result = await service.SearchAsync(options);

            if (result.Data.Count.Equals(0))
                return new Response<List<StripeTransactionData>>(null, 404, "Transação não encontrada");

            var data = new List<StripeTransactionData>();
            foreach (var item in result.Data)
            {
                data.Add(new StripeTransactionData(item.Id, item.BillingDetails.Email, item.Amount,
                                                   item.AmountCaptured, item.Status, item.Paid, item.Refunded));
            }

            return new Response<List<StripeTransactionData>>(data, 200, "Sucess");
        }
    }
}