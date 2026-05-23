namespace Logistics.Application.DTOs;

public class ClientTrackingDto
{
    public Guid ShipmentId { get; set; }
    public string OverallStatus { get; set; } = string.Empty;
    public decimal? CurrentLatitude { get; set; }
    public decimal? CurrentLongitude { get; set; }
    public List<ClientLegDto> TrackingTimeline { get; set; } = new List<ClientLegDto>();
}