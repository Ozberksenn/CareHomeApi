using CareHomeApi.Data;
using CareHomeApi.DTOs.Bill;
using CareHomeApi.Exceptions;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CareHomeApi.Services;

public class BillService : IBillService
{
    private readonly AppDbContext _context;

    public BillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BillDto>> GetAllAsync()
    {
        return await _context.Bills
            .Include(b => b.UtilityBillType)
            .Select(b => ToDto(b))
            .ToListAsync();
    }

    public async Task<BillDto?> GetByIdAsync(int id)
    {
        var bill = await _context.Bills
            .Include(b => b.UtilityBillType)
            .FirstOrDefaultAsync(b => b.Id == id);

        return bill is null ? null : ToDto(bill);
    }

    public async Task<BillDto> CreateAsync(CreateBillDto dto)
    {
        var bill = new Models.Bill
        {
            UtilityBillTypeId = dto.UtilityBillTypeId,
            Amount = dto.Amount,
            PaidDate = dto.PaidDate ?? DateOnly.FromDateTime(DateTime.UtcNow)
        };

        _context.Bills.Add(bill);
        await SaveOrThrowDuplicateAsync();

        await _context.Entry(bill).Reference(b => b.UtilityBillType).LoadAsync();

        return ToDto(bill);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBillDto dto)
    {
        var bill = await _context.Bills.FindAsync(id);
        if (bill is null)
        {
            return false;
        }

        bill.UtilityBillTypeId = dto.UtilityBillTypeId;
        bill.Amount = dto.Amount;
        bill.PaidDate = dto.PaidDate;

        await SaveOrThrowDuplicateAsync();
        return true;
    }

    private async Task SaveOrThrowDuplicateAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new DuplicateBillException("Bu fatura türü için seçilen aya ait bir ödeme kaydı zaten var.");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var bill = await _context.Bills.FindAsync(id);
        if (bill is null)
        {
            return false;
        }

        _context.Bills.Remove(bill);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BillDto ToDto(Models.Bill bill) => new()
    {
        Id = bill.Id,
        UtilityBillTypeId = bill.UtilityBillTypeId,
        UtilityBillTypeName = bill.UtilityBillType?.Name,
        Amount = bill.Amount,
        PaidDate = bill.PaidDate,
        CreatedAt = bill.CreatedAt
    };
}
