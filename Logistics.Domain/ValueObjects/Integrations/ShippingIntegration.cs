using Logistics.Domain.Enums.Responsible;

namespace Logistics.Domain.ValueObjects.Integrations;

public class ShippingIntegration
{
    public IntegrationProvider Provider { get; private set; }
    public string ApiKey { get; private set; } = string.Empty;
    public string WebhookUrl { get; private set; } = string.Empty;
    public string ApiBaseUrl { get; private set; } = string.Empty;

    public ShippingIntegration(IntegrationProvider provider, string apiKey, string webhookUrl, string apiBaseUrl)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) throw new Exception("ApiKey is Required.");
        if (string.IsNullOrWhiteSpace(apiBaseUrl)) throw new Exception("ApiBaseUrl is Required.");

        Provider = provider;
        ApiKey = apiKey;
        WebhookUrl = webhookUrl;
        ApiBaseUrl = apiBaseUrl;
    }
    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return Provider;
        yield return ApiKey;
        yield return WebhookUrl;
        yield return ApiBaseUrl;
    }
}