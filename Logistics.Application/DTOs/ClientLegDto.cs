namespace Logistics.Application.DTOs;

public class ClientLegDto
{
    public int Sequence { get; set; }
    public string Route { get; set; } = string.Empty; 
    public string Status { get; set; } = string.Empty;
    public DateTime? ActualEnd { get; set; }
}