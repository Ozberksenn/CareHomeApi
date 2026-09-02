using CareHomeApi.DTOs.CareProvider;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CareProvidersController : ControllerBase
{
    private readonly ICareProviderService _service;

    public CareProvidersController(ICareProviderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CareProviderDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CareProviderDto>> GetById(int id)
    {
        var provider = await _service.GetByIdAsync(id);
        return provider is null ? NotFound() : Ok(provider);
    }

    [HttpPost]
    public async Task<ActionResult<CareProviderDto>> Create(CreateCareProviderDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCareProviderDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
