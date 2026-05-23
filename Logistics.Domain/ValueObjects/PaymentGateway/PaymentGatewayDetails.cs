namespace Logistics.Domain.ValueObjects.PaymentGateway;

public class PaymentGatewayDetails
{
    public string Provider { get; private set; } = string.Empty;
    public string Reference { get; private set; } = string.Empty;
    public string CheckoutUrl { get; private set; } = string.Empty;

    private PaymentGatewayDetails() { }

    public PaymentGatewayDetails(string provider, string reference, string checkoutUrl)
    {
        Provider = provider;
        Reference = reference;
        CheckoutUrl = checkoutUrl;
    }
}