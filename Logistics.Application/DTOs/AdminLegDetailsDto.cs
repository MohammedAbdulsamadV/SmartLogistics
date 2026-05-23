namespace Logistics.Application.DTOs;

public class AdminLegDetailsDto
{
    public int Sequence { get; set; }
    public string TransportMode { get; set; } = string.Empty;
    public string FromHub { get; set; } = string.Empty;
    public string ToHub { get; set; } = string.Empty;
    public string LegStatus { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string ResponsibleType { get; set; } = string.Empty;
    public string ResponsiblePhone { get; set; } = string.Empty;
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
}