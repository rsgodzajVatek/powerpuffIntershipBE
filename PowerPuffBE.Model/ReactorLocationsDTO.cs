namespace PowerPuffBE.Model;

using Enums;

public class ReactorLocationsDTO
{
    public Guid Id {get; set;}
    public Guid ReactorId {get; set;}
    public double Latitude {get; set;}
    public double Longitude {get; set;}
    public string? ReactorName {get; set;}
}