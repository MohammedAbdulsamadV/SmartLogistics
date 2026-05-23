namespace Logistics.Application.DTOs;

public class PaymentInitResultDto
{
    public string CheckoutUrl { get; set; } = string.Empty;
    public string TransactionReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }

   
}