using CareHomeApi.DTOs.DutyRotation;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DutyRotationController : ControllerBase
{
    private readonly IDutyRotationService _service;

    public DutyRotationController(IDutyRotationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<DutyRotationEntryDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<ActionResult<DutyRotationEntryDto>> Add(CreateDutyRotationEntryDto dto)
    {
        var (result, entry) = await _service.AddAsync(dto);
        return result switch
        {
            AddDutyRotationResult.ProviderNotFound => NotFound("Care provider not found."),
            AddDutyRotationResult.AlreadyInRotation => Conflict("Provider is already in the rotation."),
            _ => Ok(entry)
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> Reorder(ReorderDutyRotationDto dto)
    {
        var reordered = await _service.ReorderAsync(dto);
        return reordered
            ? NoContent()
            : BadRequest("CareProviderIds must contain exactly the current rotation members.");
    }

    [HttpPut("swap")]
    public async Task<IActionResult> Swap(SwapDutyRotationDto dto)
    {
        var swapped = await _service.SwapAsync(dto);
        return swapped ? NoContent() : NotFound();
    }

    [HttpGet("current")]
    public async Task<ActionResult<CurrentDutyDto>> GetCurrent([FromQuery] DateOnly? date)
    {
        var current = await _service.GetCurrentAsync(date);
        return current is null ? NotFound("Rotation is empty.") : Ok(current);
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<List<CurrentDutyDto>>> GetUpcoming([FromQuery] int weeks = 8)
    {
        return Ok(await _service.GetUpcomingAsync(weeks));
    }
}
