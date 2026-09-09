using CareHomeApi.DTOs.DutyRotation;

namespace CareHomeApi.Services.Interfaces;

public enum AddDutyRotationResult
{
    Created,
    ProviderNotFound,
    AlreadyInRotation
}

public interface IDutyRotationService
{
    Task<List<DutyRotationEntryDto>> GetAllAsync();
    Task<(AddDutyRotationResult Result, DutyRotationEntryDto? Entry)> AddAsync(CreateDutyRotationEntryDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ReorderAsync(ReorderDutyRotationDto dto);
    Task<bool> SwapAsync(SwapDutyRotationDto dto);
    Task<CurrentDutyDto?> GetCurrentAsync(DateOnly? date);
    Task<List<CurrentDutyDto>> GetUpcomingAsync(int weeks);
}
