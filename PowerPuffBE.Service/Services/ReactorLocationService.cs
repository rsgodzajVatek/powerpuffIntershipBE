namespace PowerPuffBE.Service.Services;

using PowerPuffBE.Data.Entities;
using PowerPuffBE.Data.Repositories;
using PowerPuffBE.Model;
using PowerPuffBE.Service.Mappers;

public interface IReactorLocationService
{
    Task<IEnumerable<ReactorLocationDTO>> GetLocations();

    Task<ReactorLocationDTO> GetLocationById(Guid reactorId);
}

public class ReactorLocationService : IReactorLocationService
{
    private readonly IReactorLocationRepository _reactorLocationRepository;
    private readonly IReactorMapper _reactorMapper;
    public readonly IReactorRepository _reactorRepository;

    public ReactorLocationService(
        IReactorLocationRepository reactorLocationRepository,
        IReactorMapper reactorMapper,
        IReactorRepository reactorRepository)
    {
        _reactorLocationRepository = reactorLocationRepository;
        _reactorMapper = reactorMapper;
        _reactorRepository = reactorRepository;
    }

    public async Task<IEnumerable<ReactorLocationDTO>> GetLocations()
    {
        var locations = await _reactorLocationRepository.GetLocations();
        locations.ToList();

        return _reactorMapper.MapLocationListToDTO(locations);
    }

    public Task<ReactorLocationDTO> GetLocationById(Guid reactorId)
    {
        throw new NotImplementedException();
    }
}
