namespace PowerPuffBE.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Reactor")]
public class ReactorEntity : BaseEntity
{
    public string Name { get; set; }
    
    public int Status { get; set; }
    
    public string Description { get; set; }
    
    public Guid ImageId { get; set; }
    
    public virtual ICollection<ReactorProductionChecksEntity> ProductionChecks { get; set; }
}