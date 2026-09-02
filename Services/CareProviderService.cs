using CareHomeApi.Data;
using CareHomeApi.DTOs.CareProvider;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class CareProviderService : ICareProviderService
{
    private readonly AppDbContext _context;

    public CareProviderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CareProviderDto>> GetAllAsync()
    {
        return await _context.CareProviders
            .Select(p => ToDto(p))
            .ToListAsync();
    }

    public async Task<CareProviderDto?> GetByIdAsync(int id)
    {
        var provider = await _context.CareProviders.FindAsync(id);
        return provider is null ? null : ToDto(provider);
    }

    public async Task<CareProviderDto> CreateAsync(CreateCareProviderDto dto)
    {
        var provider = new Models.CareProvider
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            Email = dto.Email,
            MonthlySalary = dto.MonthlySalary
        };

        _context.CareProviders.Add(provider);
        await _context.SaveChangesAsync();

        return ToDto(provider);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCareProviderDto dto)
    {
        var provider = await _context.CareProviders.FindAsync(id);
        if (provider is null)
        {
            return false;
        }

        provider.FullName = dto.FullName;
        provider.Phone = dto.Phone;
        provider.Email = dto.Email;
        provider.MonthlySalary = dto.MonthlySalary;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var provider = await _context.CareProviders.FindAsync(id);
        if (provider is null)
        {
            return false;
        }

        _context.CareProviders.Remove(provider);
        await _context.SaveChangesAsync();
        return true;
    }

    private static CareProviderDto ToDto(Models.CareProvider provider) => new()
    {
        Id = provider.Id,
        FullName = provider.FullName,
        Phone = provider.Phone,
        Email = provider.Email,
        MonthlySalary = provider.MonthlySalary,
        CreatedAt = provider.CreatedAt
    };
}
