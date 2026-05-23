using Logistics.Application.Interfaces;
using Logistics.Infrastucture.Persistence;
using Logistics.Infrastucture.Repository;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Logistics.Application.Common;

namespace Logistics.Infrastucture.Services.Payments;

public class PaymentGatewayService : IPaymentGatewayService
{
    private readonly HttpClient _httpClient;

    public PaymentGatewayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<GatewayCheckoutResult> CreateCheckoutSessionAsync(Guid orderId, decimal amount, string gatewayName)
    {
        if (gatewayName.ToUpper() == "STRIPE")
        {
            return await ExecuteStripeIntegrationAsync(orderId, amount);
        }
        else if (gatewayName.ToUpper() == "PAYMOB")
        {
            return await ExecutePaymobIntegrationAsync(orderId, amount);
        }

        return new GatewayCheckoutResult { IsSuccess = false, ErrorMessage = "بوابة الدفع المحددة غير مدعومة بالسيستم." };
    }
    private async Task<GatewayCheckoutResult> ExecutePaymobIntegrationAsync(Guid orderId, decimal amount)
    {
        try
        {
            return new GatewayCheckoutResult
            {
                CheckoutUrl = "https://accept.paymob.com/api/acceptance/iframes/mock_iframe_id?payment_token=mock_token",
                TransactionReference = "pm_" + Guid.NewGuid().ToString()[..12],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new GatewayCheckoutResult { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
    private async Task<GatewayCheckoutResult> ExecuteStripeIntegrationAsync(Guid orderId, decimal amount)
    {
        try
        {
            return new GatewayCheckoutResult
            {
                CheckoutUrl = "https://checkout.stripe.com/pay/mock_session_id",
                TransactionReference = "st_" + Guid.NewGuid().ToString()[..12],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new GatewayCheckoutResult { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    
}