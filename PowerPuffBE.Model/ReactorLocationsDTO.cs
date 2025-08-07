namespace PowerPuffBE.Model;

using Enums;

public class ReactorLocationsDTO
{
    public Guid Id { get; set; }
    public string ReactorId { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }


    public IEnumerable<ReactorChartDTO> Reactorpowerproduction { get; set; }
    public IEnumerable<ReactorChartDTO> Reactorcoretemperature { get; set; }

    public List<LinkModelDTO> Links { get; set; }
}