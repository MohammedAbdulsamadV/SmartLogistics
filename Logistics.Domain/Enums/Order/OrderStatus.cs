namespace Logistics.Domain.Enums.Order;

public enum OrderStatus
{
    Pending = 1,      
    Confirmed = 2,   
    Processing = 3,   
    InTransit = 4,    
    Delivered = 5,    
    Cancelled = 6
}