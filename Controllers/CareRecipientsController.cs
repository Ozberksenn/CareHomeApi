using CareHomeApi.DTOs.CareRecipient;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CareRecipientsController : ControllerBase
{
    private readonly ICareRecipientService _service;

    public CareRecipientsController(ICareRecipientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CareRecipientDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CareRecipientDto>> GetById(int id)
    {
        var recipient = await _service.GetByIdAsync(id);
        return recipient is null ? NotFound() : Ok(recipient);
    }

    [HttpPost]
    public async Task<ActionResult<CareRecipientDto>> Create(CreateCareRecipientDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCareRecipientDto dto)
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
