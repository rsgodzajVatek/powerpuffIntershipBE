namespace PowerPuffBE.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("Images")]
public class ImageEntity : BaseEntity
{
    public byte[] Image { get; set; }
}