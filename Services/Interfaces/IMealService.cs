using CareHomeApi.DTOs.Meal;

namespace CareHomeApi.Services.Interfaces;

public interface IMealService
{
    Task<List<MealDto>> GetAllAsync();
    Task<MealDto?> GetByIdAsync(int id);
    Task<MealDto> CreateAsync(CreateMealDto dto);
    Task<bool> UpdateAsync(int id, UpdateMealDto dto);
    Task<bool> DeleteAsync(int id);
}
