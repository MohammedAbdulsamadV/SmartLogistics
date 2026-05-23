namespace Logistics.Application.DTOs;

public class LegResponse
{
    public int Sequence { get; set; }
    public string Mode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}