using Microsoft.AspNetCore.Mvc;
using SimpleApi.Data.MongoDb.Repositories;

namespace SimpleApi.Web.Api.Controllers;

[ApiController]
[Route("zone")]
public class ZoneController : ControllerBase
{
    private readonly ZoneRepository _zoneRepository;

    public ZoneController(ZoneRepository zoneRepository)
    {
        _zoneRepository = zoneRepository;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetZoneBydId(Guid id)
    {
        var zone = await _zoneRepository.GetByIdAsync(id);

        return Ok(zone);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllZones()
    {
        var zones = await _zoneRepository.GetAllAsync();

        return Ok(zones);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteZoneById(Guid id)
    {
        var result = await _zoneRepository.DeleteByIdAsync(id);

        return result ? Ok(result) : NotFound(result);
    }
}