using CareHomeApi.DTOs.UtilityBillType;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityBillTypesController : ControllerBase
{
    private readonly IUtilityBillTypeService _service;

    public UtilityBillTypesController(IUtilityBillTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<UtilityBillTypeDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UtilityBillTypeDto>> GetById(int id)
    {
        var type = await _service.GetByIdAsync(id);
        return type is null ? NotFound() : Ok(type);
    }

    [HttpPost]
    public async Task<ActionResult<UtilityBillTypeDto>> Create(CreateUtilityBillTypeDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateUtilityBillTypeDto dto)
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
