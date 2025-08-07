namespace PowerPuffBE.Service.Services;

using Data.Repositories;
using Mappers;
using Model;
using PowerPuffBE.Data.Entities;

public interface IReactorService
{
    Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false);
    Task<ReactorDTO> GetReactorWithDetails(Guid reactorId);
    Task<IEnumerable<ReactorDTO>> GetReactorWithImageList();
}

public class ReactorService : IReactorService
{
    private readonly IReactorRepository _reactorRepository;
    private readonly IReactorMapper _reactorMapper;
    private readonly IImageRepository _imageRepository;

    public ReactorService(
        IReactorRepository reactorRepository,
        IReactorMapper reactorMapper,
        IImageRepository imageRepository)
    {
        _reactorRepository = reactorRepository;
        _reactorMapper = reactorMapper;
        _imageRepository = imageRepository;
    }

    public async Task<IEnumerable<ReactorDTO>> GetAllReactors(bool extended = false)
    {
        var reactors = await _reactorRepository.GetAllReactors(true);
        reactors.ToList();

        List<Tuple<ReactorEntity, ImageEntity>> entityTupleList = new List<Tuple<ReactorEntity, ImageEntity>>();

        foreach (var reactor in reactors)
        {
            var image = await _imageRepository.GetImageById(reactor.ImageId);
            Tuple<ReactorEntity, ImageEntity> tuple = new Tuple<ReactorEntity, ImageEntity>(reactor, image);
            entityTupleList.Add(tuple);
        }

        return _reactorMapper.MapListToDTO(entityTupleList);
    }

    public async Task<ReactorDTO> GetReactorWithDetails(Guid reactorId)
    {
        var reactor = await _reactorRepository.GetReactorExtendedById(reactorId);

        var image = await _imageRepository.GetImageById(reactor.ImageId);

        return _reactorMapper.MapToDTOWithDetails(new Tuple<ReactorEntity,ImageEntity>(reactor,image));
    }

    public async Task<IEnumerable<ReactorDTO>> GetReactorWithImageList()
    {
        var returnDtoList = new List<ReactorDTO>();
        var reactorsWithImages = await _reactorRepository.GetReactorImageList();
        var images = await _imageRepository.GetImages();
        foreach (var reactor in reactorsWithImages)
        {
            returnDtoList.Add(_reactorMapper.MapToDTOWithImage(reactor,
                images.FirstOrDefault(i => i.Id.Equals(reactor.ImageId))));
        }

        return returnDtoList;
    }
}