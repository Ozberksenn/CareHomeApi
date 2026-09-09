using CareHomeApi.DTOs.UtilityBillType;

namespace CareHomeApi.Services.Interfaces;

public interface IUtilityBillTypeService
{
    Task<List<UtilityBillTypeDto>> GetAllAsync();
    Task<UtilityBillTypeDto?> GetByIdAsync(int id);
    Task<UtilityBillTypeDto> CreateAsync(CreateUtilityBillTypeDto dto);
    Task<bool> UpdateAsync(int id, UpdateUtilityBillTypeDto dto);
    Task<bool> DeleteAsync(int id);
}
