using Logistics.Application.Common;

namespace Logistics.Application.Interfaces;

public interface IPaymentGatewayService 
{
    Task<GatewayCheckoutResult> CreateCheckoutSessionAsync(Guid orderId, decimal amount, string gatewayName);

    
    
}