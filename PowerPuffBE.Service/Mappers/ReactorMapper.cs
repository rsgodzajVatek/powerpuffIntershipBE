namespace PowerPuffBE.Service.Mappers;

using Data.Entities;
using Model;
using Model.Enums;
using static System.Net.Mime.MediaTypeNames;

public interface IReactorMapper
{
    ReactorDTO MapToDTO(ReactorEntity entity);
    IEnumerable<ReactorDTO> MapListToDTO(List<ReactorEntity> entityList);
    ReactorDTO MapToDTOWithDetails(ReactorEntity entity);
    ReactorDTO MapToDTOWithImage(ReactorEntity reactor, ImageEntity image);
}

public class ReactorMapper : IReactorMapper
{
    public ReactorDTO MapToDTO(ReactorEntity entity)
    {
        return new ReactorDTO()
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Status = ((ReactorStatusEnum)entity.Status).ToString().ToLower(),
            Links = new List<LinkModelDTO>()
        };
    }

    public IEnumerable<ReactorDTO> MapListToDTO(List<ReactorEntity> entityList)
    {
        return entityList.Select(MapToDTOWithDetails);
    }

    public ReactorDTO MapToDTOWithDetails(ReactorEntity entity)
    {
        return new ReactorDTO()
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            Status = ((ReactorStatusEnum)entity.Status).ToString().ToLower(),
            ImageContent = null,

            Reactorpowerproduction = MapProductionChecks(entity.ProductionChecks, entity.Status),
            Reactorcoretemperature = MapTemperatureChecks(entity.ProductionChecks, entity.Status),

            Links = new List<LinkModelDTO>()
        };
    }

    public ReactorDTO MapToDTOWithImage(ReactorEntity reactor, ImageEntity image)
    {
        return new ReactorDTO()
        {
            Id = reactor.Id,
            Name = reactor.Name,
            Description = reactor.Description,
            ImageContent = image == null ? "No image found" : "data:image/png;base64," + Convert.ToBase64String(image.Image)
        };
    }

    // 🔧 Metody prywatne do mapowania wykresów
    private IEnumerable<ReactorChartDTO> MapProductionChecks(IEnumerable<ReactorProductionChecksEntity>? checks, int status)
    {
        return checks?
            .Where(p => p.PowerProduction > 0)
            .Select(p => new ReactorChartDTO
            {
                Time = new DateTimeOffset(p.MeasureTime).ToUnixTimeMilliseconds(),
                Value = p.PowerProduction,
                Status = (ReactorStatusEnum)status
            }) ?? Enumerable.Empty<ReactorChartDTO>();
    }

    private IEnumerable<ReactorChartDTO> MapTemperatureChecks(IEnumerable<ReactorProductionChecksEntity>? checks, int status)
    {
        return checks?
            .Where(p => p.Temperature > 0)
            .Select(p => new ReactorChartDTO
            {
                Time = new DateTimeOffset(p.MeasureTime).ToUnixTimeMilliseconds(),
                Value = p.Temperature,
                Status = (ReactorStatusEnum)status
            }) ?? Enumerable.Empty<ReactorChartDTO>();
    }
}
