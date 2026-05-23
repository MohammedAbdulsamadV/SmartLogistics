namespace Logistics.Application.DTOs;

public class ClientLegDto
{
    public int Sequence { get; set; }
    public string Route { get; set; } = string.Empty; // من فين لفين (StartHub -> EndHub)
    public string Status { get; set; } = string.Empty;
    public DateTime? ActualEnd { get; set; }
}