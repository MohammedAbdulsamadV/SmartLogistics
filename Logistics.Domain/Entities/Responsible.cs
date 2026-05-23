using Logistics.Domain.Common;
using Logistics.Domain.Enums.Responsible;
using Logistics.Domain.ValueObjects.Integrations;

namespace Logistics.Domain.Entities;

public class Responsible : BaseEntity
{
    public string Name { get; private set; }
    public ResponsibleType Type { get; private set; } // Enum: Driver, ShippingCompany, CustomsAgent, Warehouse
    public string ContactPhone { get; private set; }
    public string Email { get; private set; }
    public ShippingIntegration? Integration { get; private set; }
    public bool? IsActive { get; set; }
    public string? TaxNumber { get; private set; } 

    private Responsible() { }

    public Responsible(
        string name, 
        ResponsibleType type, 
        string contactPhone, 
        string email, 
        string? taxNumber = null, 
        ShippingIntegration? integration = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Responsible name is Required.");
        if (string.IsNullOrWhiteSpace(contactPhone)) throw new Exception("Contact phone is Required.");
        if (string.IsNullOrWhiteSpace(email)) throw new Exception("Email is Required.");

        Name = name;
        Type = type;
        ContactPhone = contactPhone;
        Email = email;
        TaxNumber = taxNumber;
        Integration = integration;
        IsActive = true;
    }
    public void UpdateDetails(string name, string contactPhone, string email, string? taxNumber, ShippingIntegration? integration)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new Exception("Responsible name is Required.");
        
        Name = name;
        ContactPhone = contactPhone;
        Email = email;
        TaxNumber = taxNumber;
        Integration = integration;
    }
   
}