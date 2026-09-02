using CareHomeApi.DTOs.CareProvider;

namespace CareHomeApi.Services.Interfaces;

public interface ICareProviderService
{
    Task<List<CareProviderDto>> GetAllAsync();
    Task<CareProviderDto?> GetByIdAsync(int id);
    Task<CareProviderDto> CreateAsync(CreateCareProviderDto dto);
    Task<bool> UpdateAsync(int id, UpdateCareProviderDto dto);
    Task<bool> DeleteAsync(int id);
}
