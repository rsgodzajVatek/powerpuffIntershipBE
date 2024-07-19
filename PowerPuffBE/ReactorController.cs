namespace PowerPuffBE;

using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class ReactorController : ControllerBase
{
    private readonly IReactorService _reactorService;

    public ReactorController(IReactorService reactorService)
    {
        _reactorService = reactorService;
    }

    [HttpGet]
    public async Task<IEnumerable<ReactorDTO>> GetAllReactors()
    {
        return await _reactorService.GetAllReactors();
    }
}