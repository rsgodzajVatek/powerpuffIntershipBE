namespace PowerPuffBE.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("ReactorProductionChecks")]
public class ReactorProductionChecksEntity : BaseEntity
{
    [ForeignKey("ReactorId")]
    public Guid ReactorId { get; set; }
    
    public DateTime MeasureTime { get; set; }
    
    public int Temperature { get; set; }
    
    public int PowerProduction { get; set; }
    
    public virtual ReactorEntity Reactor { get; set; }
}