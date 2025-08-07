using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerPuffBE.Data.Entities
{
    [Table("Location")]
    public class ReactorLocationEntity : BaseEntity
    {
        [ForeignKey("ReactorId")]
        public Guid ReactorId { get; set; }

        public double latitude { get; set; } 

        public double longitude { get; set; }
    }
}
