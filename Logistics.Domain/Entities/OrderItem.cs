using Logistics.Domain.Common;

namespace Logistics.Domain.Entities;

public class OrderItem : BaseEntity
{
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; set; }
    public OrderItem(string productName, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name is invalid"); 
        }else if (quantity <= 0)
        {
            throw new ArgumentException("Quantity is must be greater than zero");
        }
        ProductName = productName;
        Price = price;
        Quantity = quantity;
        TotalPrice = Quantity * price;
    }

    
}