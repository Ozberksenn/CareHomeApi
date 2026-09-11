using CareHomeApi.DTOs.Bill;

namespace CareHomeApi.Services.Interfaces;

public interface IBillService
{
    Task<List<BillDto>> GetAllAsync();
    Task<MonthlyBillsDto> GetByMonthAsync(int? year, int? month);
    Task<BillDto?> GetByIdAsync(int id);
    Task<BillDto> CreateAsync(CreateBillDto dto);
    Task<bool> UpdateAsync(int id, UpdateBillDto dto);
    Task<bool> DeleteAsync(int id);
}
