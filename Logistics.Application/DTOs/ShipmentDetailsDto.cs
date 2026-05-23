namespace Logistics.Application.DTOs;

public class ShipmentDetailsDto
{
    public Guid ShipmentId { get; set; }
    public string OverallStatus { get; set; } = string.Empty;
    public decimal VolumetricWeight { get; set; }
    public decimal? CurrentLat { get; set; }
    public decimal? CurrentLng { get; set; }
    public List<LegDto> Legs { get; set; } = new List<LegDto>();
}