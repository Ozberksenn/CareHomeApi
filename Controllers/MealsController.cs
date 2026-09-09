using CareHomeApi.DTOs.Meal;
using CareHomeApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private readonly IMealService _service;

    public MealsController(IMealService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<MealDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MealDto>> GetById(int id)
    {
        var meal = await _service.GetByIdAsync(id);
        return meal is null ? NotFound() : Ok(meal);
    }

    [HttpPost]
    public async Task<ActionResult<MealDto>> Create(CreateMealDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateMealDto dto)
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
