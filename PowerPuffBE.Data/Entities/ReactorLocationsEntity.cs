namespace PowerPuffBE.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("ReactorLocations")]
public class ReactorLocationsEntity : BaseEntity
{
    [ForeignKey("ReactorId")]
    public Guid ReactorId { get; set; }
    
    public double Longitude { get; set; }
    
    public double Latitude { get; set; }

    public virtual ReactorEntity? Reactor {get; set;}
}