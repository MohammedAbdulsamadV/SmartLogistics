namespace Logistics.Domain.Exceptions.Order;

public class OrderDomainException : Exception
{
    public OrderDomainException(string message) : base(message)
    {
        
    }
}