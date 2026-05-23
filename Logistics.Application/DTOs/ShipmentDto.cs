namespace Logistics.Application.DTOs;

public class ShipmentDto
{
    public Guid Id  { get; set; }
    public string Status  { get; set; } = string.Empty; 
    public decimal Lat { get; set; }
    public decimal Lng  { get; set; }
    public List<LegDto> Legs { get; set; } = new();
}