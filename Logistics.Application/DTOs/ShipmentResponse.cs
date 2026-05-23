namespace Logistics.Application.DTOs;

public class ShipmentResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public List<LegResponse> Legs { get; set; } = new();
}