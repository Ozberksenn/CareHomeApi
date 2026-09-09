using CareHomeApi.DTOs.Bill;
using CareHomeApi.Exceptions;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillsController : ControllerBase
{
    private readonly IBillService _service;

    public BillsController(IBillService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<BillDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BillDto>> GetById(int id)
    {
        var bill = await _service.GetByIdAsync(id);
        return bill is null ? NotFound() : Ok(bill);
    }

    [HttpPost]
    public async Task<ActionResult<BillDto>> Create(CreateBillDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (DuplicateBillException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBillDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        catch (DuplicateBillException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
