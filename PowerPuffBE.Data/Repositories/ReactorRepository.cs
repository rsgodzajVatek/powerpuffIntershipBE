namespace PowerPuffBE.Data.Repositories;

using Entities;
using Microsoft.EntityFrameworkCore;

public interface IReactorRepository
{
    Task<IEnumerable<ReactorEntity>> GetAllReactors();
    Task<ReactorEntity> GetReactorExtendedById(Guid id);
}
public class ReactorRepository : IReactorRepository
{
    private readonly PowerPuffDbContext _context;

    public ReactorRepository(PowerPuffDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReactorEntity>> GetAllReactors()
    {
        return await _context.Reactors.ToListAsync();
    }

    public async Task<ReactorEntity> GetReactorExtendedById(Guid id)
    {
        return await _context.Reactors
            .Include(r => r.ProductionChecks)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}