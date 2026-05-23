namespace Logistics.Application.DTOs;

public class AdminShipmentDetailsDto
{
    public Guid ShipmentId { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal VolumetricWeight { get; set; }
    public string? RoutePolyline { get; set; }
    public decimal? CurrentLat { get; set; }
    public decimal? CurrentLng { get; set; }
    public List<AdminLegDetailsDto> DetailedLegs { get; set; } = new List<AdminLegDetailsDto>();
}
