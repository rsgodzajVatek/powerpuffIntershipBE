namespace PowerPuffBE.Data.Repositories;

using Entities;
using Microsoft.EntityFrameworkCore;

public interface IImageRepository
{
    Task<ImageEntity> GetImageById(Guid id);
}

public class ImageRepository : IImageRepository
{
    private readonly PowerPuffDbContext _context;

    public ImageRepository(PowerPuffDbContext context)
    {
        _context = context;
    }

    public async Task<ImageEntity> GetImageById(Guid id)
    {
        return await _context.Image.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}