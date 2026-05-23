namespace Logistics.Application.Common;

public class GatewayCheckoutResult
{
    public string CheckoutUrl { get; set; } = string.Empty;          
    public string TransactionReference { get; set; } = string.Empty;  
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}