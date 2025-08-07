namespace PowerPuffBE.Service.Mappers;

using PowerPuffBE.Data.Entities;
using PowerPuffBE.Model;
using PowerPuffBE.Model.Enums;

public interface IReactorMapper
{
    ReactorDTO MapToDTO(ReactorEntity entity);
    IEnumerable<ReactorDTO> MapListToDTO(List<ReactorEntity> entityList);
    ReactorDTO MapToDTOWithDetails(ReactorEntity entity);
    ReactorDTO MapToDTOWithImage(ReactorEntity reactor, ImageEntity image);
    ReactorLocationsDTO MapLocationToDTO(ReactorLocationsEntity entity);
    IEnumerable<ReactorLocationsDTO> MapLocationListToDTO(IEnumerable<ReactorLocationsEntity> entityList);
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
            Status = CalculateOverallStatus(entity).Select(s => (int)s).ToList()
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
            Status = CalculateOverallStatus(entity).Select(s => (int)s).ToList(),
            Reactorpowerproduction = MapToChartDTO(entity.ProductionChecks, pc => pc.PowerProduction, value => CalculateStatus(value, 50, 250, 10, 300)),
            Reactorcoretemperature = MapToChartDTO(entity.ProductionChecks, pc => pc.Temperature, value => CalculateStatus(value, 400, 800, 250, 950)),
            
            ImageContent = null, 
            Links = new List<LinkModelDTO>
            {
                new LinkModelDTO { Label = "View Details", Href = $"/reactors/{entity.Id}/details" },
                new LinkModelDTO { Label = "Maintenance Log", Href = $"/reactors/{entity.Id}/maintenance" },
                new LinkModelDTO { Label = "Performance Report", Href = $"/reactors/{entity.Id}/report" }
            }
        };
    }

    public ReactorDTO MapToDTOWithImage(ReactorEntity reactor, ImageEntity image)
    {
        return new ReactorDTO()
        {
            Id = reactor.Id,
            Name = reactor.Name,
            Description = reactor.Description,
            Status = CalculateOverallStatus(reactor).Select(s => (int)s).ToList(),
            ImageContent = image == null ? "No image found" : "data:image/png;base64," + Convert.ToBase64String(image.Image),
            
            Reactorpowerproduction = MapToChartDTO(reactor.ProductionChecks, pc => pc.PowerProduction, value => CalculateStatus(value, 50, 250, 10, 300)),
            Reactorcoretemperature = MapToChartDTO(reactor.ProductionChecks, pc => pc.Temperature, value => CalculateStatus(value, 400, 800, 250, 950)),
            Links = new List<LinkModelDTO>()
        };
    }

    private IEnumerable<ReactorChartDTO> MapToChartDTO(
        ICollection<ReactorProductionChecksEntity> productionChecks, 
        Func<ReactorProductionChecksEntity, int> valueSelector,
        Func<int, ReactorStatusEnum> statusCalculator)
    {
        if (productionChecks == null)
            return new List<ReactorChartDTO>();

        return productionChecks.Select(pc => new ReactorChartDTO
        {
            Time = ((DateTimeOffset)pc.MeasureTime).ToString("yyyy-MM-ddTHH:mm:ss"),
            Value = valueSelector(pc), 
            Status = statusCalculator(valueSelector(pc)) 
        }).OrderBy(x => x.Time).ToList();
    }

    private List<ReactorStatusEnum> CalculateOverallStatus(ReactorEntity entity)
    {
        var statusList = new List<ReactorStatusEnum>();
        
        if (entity.ProductionChecks != null && entity.ProductionChecks.Any())
        {
            var latestCheck = entity.ProductionChecks.OrderByDescending(pc => pc.MeasureTime).First();
            var powerStatus = CalculateStatus(latestCheck.PowerProduction, 50, 250, 10, 300);
            statusList.Add(powerStatus);
            var temperatureStatus = CalculateStatus(latestCheck.Temperature, 400, 800, 250, 950);
            statusList.Add(temperatureStatus);
        }
        else
        {
            statusList.Add(ReactorStatusEnum.OutOfRange);
            statusList.Add(ReactorStatusEnum.OutOfRange);
        }
        
        return statusList;
    }

    private ReactorStatusEnum CalculateStatus(int value, int inRangeMin, int inRangeMax, int criticalMin, int criticalMax)
    {
        if (value >= inRangeMin && value <= inRangeMax)
            return ReactorStatusEnum.InRange;
        
        else if (value <= criticalMin || value >= criticalMax)
            return ReactorStatusEnum.Critical;
        
        return ReactorStatusEnum.OutOfRange;
    }

    public ReactorLocationsDTO MapLocationToDTO(ReactorLocationsEntity entity)
    {
        return new ReactorLocationsDTO
        {
            Id = entity.Id,
            ReactorId = entity.ReactorId,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            ReactorName = entity.Reactor?.Name
        };
    }

    public IEnumerable<ReactorLocationsDTO> MapLocationListToDTO(IEnumerable<ReactorLocationsEntity> entityList)
    {
        return entityList.Select(MapLocationToDTO);
    }
}