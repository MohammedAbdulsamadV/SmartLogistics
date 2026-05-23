using Logistics.Domain.Common;
using Logistics.Domain.Enums.Customer;

namespace Logistics.Domain.Entities;

public class Customer : BaseEntity
{
    
    public string Name { get; private set; } = String.Empty;
    public string PhoneNumber { get; private set; } = String.Empty;
    public string Email { get; private set; } = String.Empty;
    public CustomerType Type { get; private set; }
    public string TaxNumber { get; private set; } = String.Empty;
    public decimal CreditLimit { get; private set; }
    public decimal CurrentBalance { get; private set; }
    private Customer() { }
    public Customer(string name, string email, CustomerType type, string phoneNumber = "", string taxNumber = "", decimal creditLimit = 0)
    {
        Name = name;
        Email = email;
        Type = type;
        PhoneNumber = phoneNumber;
        TaxNumber = taxNumber;
        CreditLimit = creditLimit;
        CurrentBalance = 0; 
    }
    public void UpdateBalance(decimal amount)
    {
        CurrentBalance += amount;
    }
    
}