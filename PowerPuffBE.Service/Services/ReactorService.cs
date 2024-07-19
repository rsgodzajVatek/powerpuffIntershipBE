namespace PowerPuffBE.Service.Services;

using Data.Repositories;
using Mappers;
using Model;

public interface IReactorService
{
    Task<IEnumerable<ReactorDTO>> GetAllReactors();
    Task<ReactorDTO> GetReactorWithDetails(Guid reactorId);
}

public class ReactorService : IReactorService
{
    private readonly IReactorRepository _reactorRepository;
    private readonly IReactorMapper _reactorMapper;

    public ReactorService(IReactorRepository reactorRepository, IReactorMapper reactorMapper)
    {
        _reactorRepository = reactorRepository;
        _reactorMapper = reactorMapper;
    }

    public async Task<IEnumerable<ReactorDTO>> GetAllReactors()
    {
        var reactors = await _reactorRepository.GetAllReactors();
        return _reactorMapper.MapListToDTO(reactors.ToList());
    }

    public async Task<ReactorDTO> GetReactorWithDetails(Guid reactorId)
    {
        var reactor = await _reactorRepository.GetReactorExtendedById(reactorId);
        return _reactorMapper.MapToDTOWithDetails(reactor);
    }
}