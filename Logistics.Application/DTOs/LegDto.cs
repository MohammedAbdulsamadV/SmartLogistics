namespace Logistics.Application.DTOs;

public class LegDto
{
    public int Sequence { get; set; }
    public string Mode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string ResponsibleType { get; set; } = string.Empty;
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
}