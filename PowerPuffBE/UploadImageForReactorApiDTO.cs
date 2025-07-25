using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace PowerPuffBE;

public class UploadImageForReactorApiDTO
{
    [Required]
    public Guid ReactorId { get; set; }
    
    public IFormFile Image { get; set; }
}