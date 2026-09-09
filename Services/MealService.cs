using CareHomeApi.Data;
using CareHomeApi.DTOs.Meal;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class MealService : IMealService
{
    private readonly AppDbContext _context;

    public MealService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MealDto>> GetAllAsync()
    {
        return await _context.Meals
            .Include(m => m.Provider)
            .Select(m => ToDto(m))
            .ToListAsync();
    }

    public async Task<MealDto?> GetByIdAsync(int id)
    {
        var meal = await _context.Meals
            .Include(m => m.Provider)
            .FirstOrDefaultAsync(m => m.Id == id);

        return meal is null ? null : ToDto(meal);
    }

    public async Task<MealDto> CreateAsync(CreateMealDto dto)
    {
        var meal = new Models.Meal
        {
            ProviderId = dto.ProviderId,
            Date = dto.Date,
            Description = dto.Description
        };

        _context.Meals.Add(meal);
        await _context.SaveChangesAsync();

        if (meal.ProviderId is not null)
        {
            await _context.Entry(meal).Reference(m => m.Provider).LoadAsync();
        }

        return ToDto(meal);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMealDto dto)
    {
        var meal = await _context.Meals.FindAsync(id);
        if (meal is null)
        {
            return false;
        }

        meal.Date = dto.Date;
        meal.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var meal = await _context.Meals.FindAsync(id);
        if (meal is null)
        {
            return false;
        }

        _context.Meals.Remove(meal);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MealDto ToDto(Models.Meal meal) => new()
    {
        Id = meal.Id,
        ProviderId = meal.ProviderId,
        ProviderFullName = meal.Provider?.FullName,
        Date = meal.Date,
        Description = meal.Description,
        CreatedAt = meal.CreatedAt
    };
}
