namespace Logistics.Application.DTOs;

public class AddressDto
{
    public string Street { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public AddressDto(string street, string state, string city, string zipCode, string country)
    {
        Street = street;
        State = state;
        City = city;
        ZipCode = zipCode;
        Country = country;
    }
}