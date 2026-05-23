namespace Logistics.Application.DTOs;
public class OrderItemResponse
{
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalItemPrice { get; set; }
}