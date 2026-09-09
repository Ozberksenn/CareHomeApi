using CareHomeApi.Data;
using CareHomeApi.DTOs.UtilityBillType;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class UtilityBillTypeService : IUtilityBillTypeService
{
    private readonly AppDbContext _context;

    public UtilityBillTypeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UtilityBillTypeDto>> GetAllAsync()
    {
        return await _context.UtilityBillTypes
            .Select(t => ToDto(t))
            .ToListAsync();
    }

    public async Task<UtilityBillTypeDto?> GetByIdAsync(int id)
    {
        var type = await _context.UtilityBillTypes.FindAsync(id);
        return type is null ? null : ToDto(type);
    }

    public async Task<UtilityBillTypeDto> CreateAsync(CreateUtilityBillTypeDto dto)
    {
        var type = new Models.UtilityBillType
        {
            Name = dto.Name
        };

        _context.UtilityBillTypes.Add(type);
        await _context.SaveChangesAsync();

        return ToDto(type);
    }

    public async Task<bool> UpdateAsync(int id, UpdateUtilityBillTypeDto dto)
    {
        var type = await _context.UtilityBillTypes.FindAsync(id);
        if (type is null)
        {
            return false;
        }

        type.Name = dto.Name;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var type = await _context.UtilityBillTypes.FindAsync(id);
        if (type is null)
        {
            return false;
        }

        _context.UtilityBillTypes.Remove(type);
        await _context.SaveChangesAsync();
        return true;
    }

    private static UtilityBillTypeDto ToDto(Models.UtilityBillType type) => new()
    {
        Id = type.Id,
        Name = type.Name,
        CreatedAt = type.CreatedAt
    };
}
