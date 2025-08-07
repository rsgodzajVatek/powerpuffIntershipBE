namespace PowerPuffBE.Service.Mappers;

using Data.Entities;
using Model;
using Model.Enums;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

public interface IReactorMapper
{
    ReactorDTO MapToDTO(ReactorEntity entity);
    IEnumerable<ReactorDTO> MapListToDTO(List<Tuple<ReactorEntity, ImageEntity>> entityList);
    ReactorDTO MapToDTOWithDetails(Tuple<ReactorEntity, ImageEntity> entityTuple);
    ReactorDTO MapToDTOWithImage(ReactorEntity reactor,ImageEntity image);
    IEnumerable<ReactorLocationDTO> MapLocationListToDTO(IEnumerable<ReactorLocationEntity> locations);

    //IEnumerable<ReactorChartDTO> MapTempToChartDTO(ReactorEntity reactor);
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
            Status = ((ReactorStatusEnum)entity.Status).ToString().ToLower()

        };
    }

    public IEnumerable<ReactorDTO> MapListToDTO(List<Tuple<ReactorEntity, ImageEntity>> entityList)
    {
        return entityList.Select(MapToDTOWithDetails);
    }


    public ReactorDTO MapToDTOWithDetails(Tuple<ReactorEntity, ImageEntity> entityTuple)
    {

        return new ReactorDTO()
        {
            Id = entityTuple.Item1.Id,
            Description = entityTuple.Item1.Description,
            Name = entityTuple.Item1.Name,
            Status = ((ReactorStatusEnum)entityTuple.Item1.Status).ToString().ToLower(),
            Reactorcoretemperature = MapTempToChartDTO(entityTuple.Item1),
            Reactorpowerproduction = MapProdToChartDTO(entityTuple.Item1),
            ImageContent = entityTuple.Item2 == null ? "No image found" : "data:image/png;base64," + Convert.ToBase64String(entityTuple.Item2.Image)
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

    private IEnumerable<ReactorChartDTO> MapProdToChartDTO(ReactorEntity reactor)
    {
        List<ReactorChartDTO> output = new List<ReactorChartDTO>();

        foreach (ReactorProductionChecksEntity item in reactor.ProductionChecks)
        {
            ReactorChartDTO chart = new ReactorChartDTO();

            chart.Time = item.MeasureTime.Ticks;
            chart.Value = item.Temperature;
            chart.Status = (ReactorStatusEnum)reactor.Status; //TODO calc status

            output.Add(chart);
        }

        return output;
    }

    private IEnumerable<ReactorChartDTO> MapTempToChartDTO(ReactorEntity reactor)
    {
        List<ReactorChartDTO> output = new List<ReactorChartDTO>();

        foreach (ReactorProductionChecksEntity item in reactor.ProductionChecks)
        {
            ReactorChartDTO chart = new ReactorChartDTO();

            chart.Time = item.MeasureTime.Ticks;
            chart.Value = item.PowerProduction;
            chart.Status = (ReactorStatusEnum)reactor.Status; //TODO calc status

            output.Add(chart);
        }

        return output;
    }

    public IEnumerable<ReactorLocationDTO> MapLocationListToDTO(IEnumerable<ReactorLocationEntity> locations)
    {
        List<ReactorLocationDTO> output = new List<ReactorLocationDTO>();
        foreach (ReactorLocationEntity location in locations)
        {
            ReactorLocationDTO locationDTO = new ReactorLocationDTO();

            locationDTO.longitude = location.longitude;
            locationDTO.latitude = location.latitude;
            locationDTO.Id = location.Id;
            locationDTO.reactorId = location.ReactorId;

            output.Add(locationDTO);
        }
        return output; 
    }
}


