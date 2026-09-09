using CareHomeApi.Data;
using CareHomeApi.DTOs.CareRecipient;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class CareRecipientService : ICareRecipientService
{
    private readonly AppDbContext _context;

    public CareRecipientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CareRecipientDto>> GetAllAsync()
    {
        return await _context.CareRecipients
            .Select(r => ToDto(r))
            .ToListAsync();
    }

    public async Task<CareRecipientDto?> GetByIdAsync(int id)
    {
        var recipient = await _context.CareRecipients.FindAsync(id);
        return recipient is null ? null : ToDto(recipient);
    }

    public async Task<CareRecipientDto> CreateAsync(CreateCareRecipientDto dto)
    {
        var recipient = new Models.CareRecipient
        {
            FullName = dto.FullName,
            BirthDate = dto.BirthDate,
            MonthlySalary = dto.MonthlySalary
        };

        _context.CareRecipients.Add(recipient);
        await _context.SaveChangesAsync();

        return ToDto(recipient);
    }

    public async Task<bool> UpdateAsync(int id, UpdateCareRecipientDto dto)
    {
        var recipient = await _context.CareRecipients.FindAsync(id);
        if (recipient is null)
        {
            return false;
        }

        recipient.FullName = dto.FullName;
        recipient.BirthDate = dto.BirthDate;
        recipient.MonthlySalary = dto.MonthlySalary;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var recipient = await _context.CareRecipients.FindAsync(id);
        if (recipient is null)
        {
            return false;
        }

        _context.CareRecipients.Remove(recipient);
        await _context.SaveChangesAsync();
        return true;
    }

    private static CareRecipientDto ToDto(Models.CareRecipient recipient) => new()
    {
        Id = recipient.Id,
        FullName = recipient.FullName,
        BirthDate = recipient.BirthDate,
        MonthlySalary = recipient.MonthlySalary,
        CreatedAt = recipient.CreatedAt
    };
}
