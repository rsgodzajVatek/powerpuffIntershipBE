namespace PowerPuffBE.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using PowerPuffBE.Data.Entities;

public interface IReactorLocationRepository
{
    Task<IEnumerable<ReactorLocationEntity>> GetLocations();
    Task<ReactorLocationEntity> GetLocationById(Guid reactorId);
}

public class ReactorLocationRepository : IReactorLocationRepository
{
    private readonly PowerPuffDbContext _context;

    public ReactorLocationRepository(PowerPuffDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReactorLocationEntity>> GetLocations()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<ReactorLocationEntity> GetLocationById(Guid reactorId)
    {
        return await _context.Locations.FirstOrDefaultAsync(x => x.ReactorId.Equals(reactorId));
    }
}

