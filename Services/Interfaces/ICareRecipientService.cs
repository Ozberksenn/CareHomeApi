using CareHomeApi.DTOs.CareRecipient;

namespace CareHomeApi.Services.Interfaces;

public interface ICareRecipientService
{
    Task<List<CareRecipientDto>> GetAllAsync();
    Task<CareRecipientDto?> GetByIdAsync(int id);
    Task<CareRecipientDto> CreateAsync(CreateCareRecipientDto dto);
    Task<bool> UpdateAsync(int id, UpdateCareRecipientDto dto);
    Task<bool> DeleteAsync(int id);
}
