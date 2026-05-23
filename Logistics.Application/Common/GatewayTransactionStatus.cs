namespace Logistics.Application.Common;

public class GatewayTransactionStatus
{
    public string TransactionReference { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // SUCCESS, FAILED, PENDING
    public string FailureReason { get; set; } = string.Empty;
}