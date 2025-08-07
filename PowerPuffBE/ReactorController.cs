namespace PowerPuffBE;

using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class ReactorController : ControllerBase
{
    private readonly IReactorService _reactorService;
    private readonly IReactorLocationService _reactorLocationService;

    public ReactorController(IReactorService reactorService, IReactorLocationService reactorLocationService)
    {
        _reactorService = reactorService;
        _reactorLocationService = reactorLocationService;
    }

    [HttpGet]
    public async Task<IEnumerable<ReactorDTO>> GetAllReactors()
    {
        return await _reactorService.GetAllReactors(true);
    }

    [HttpGet]
    [Route("image-list")]
    public async Task<IEnumerable<ReactorDTO>> GetReactorImagesList()
    {
        var reactors = await _reactorService.GetReactorWithImageList();
        return reactors;
    }

    [HttpGet]
    [Route("reactor-locations")]
    public async Task<IEnumerable<ReactorLocationDTO>> GetReactorLocations()
    {
        var locations = await _reactorLocationService.GetLocations();
        return locations;
    }
}