namespace PowerPuffBE;

using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetImages()
    {
        var images = await _imageService.GetImages();
        return Ok(images);
    }

    [HttpPost]
    [Route("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("File not send.");

        byte[] imageData;

        using (var ms = new MemoryStream())
        {
            await imageFile.CopyToAsync(ms);
            imageData = ms.ToArray();
        }

        var imageId = await _imageService.UploadImage(imageFile.FileName, imageData);

        return Ok(imageId);
    }
    
    [HttpPost]
    [Route("upload-reactor/{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadReactorImage([FromForm] UploadImageForReactorApiDTO uploadImageForReactorApiDTO)
    {
        if (uploadImageForReactorApiDTO.Image == null || uploadImageForReactorApiDTO.Image.Length == 0)
            return BadRequest("File not send.");

        byte[] imageData;

        using (var ms = new MemoryStream())
        {
            await uploadImageForReactorApiDTO.Image.CopyToAsync(ms);
            imageData = ms.ToArray();
        }

        await _imageService.UploadForReactor(uploadImageForReactorApiDTO.ReactorId, uploadImageForReactorApiDTO.Image.Name ,imageData);
        return Ok();
    }
}