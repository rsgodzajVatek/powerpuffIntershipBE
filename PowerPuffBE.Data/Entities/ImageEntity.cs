namespace PowerPuffBE.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Images")]
public class ImageEntity : BaseEntity
{
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public Guid? ReactorId { get; set; }
}