using CareHomeApi.Data;
using CareHomeApi.DTOs.Bill;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            .Include(b => b.Recipient)
            .Select(b => ToDto(b))
            .ToListAsync();
    }

    public async Task<BillDto?> GetByIdAsync(int id)
    {
        var bill = await _context.Bills
            .Include(b => b.Recipient)
            .FirstOrDefaultAsync(b => b.Id == id);

        return bill is null ? null : ToDto(bill);
    }

    public async Task<BillDto> CreateAsync(CreateBillDto dto)
    {
        var bill = new Models.Bill
        {
            RecipientId = dto.RecipientId,
            Description = dto.Description,
            Amount = dto.Amount,
            IsPaid = dto.IsPaid,
            PaidDate = dto.PaidDate ?? DateOnly.FromDateTime(DateTime.UtcNow)
        };

        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        if (bill.RecipientId is not null)
        {
            await _context.Entry(bill).Reference(b => b.Recipient).LoadAsync();
        }

        return ToDto(bill);
    }

    public async Task<bool> UpdateAsync(int id, UpdateBillDto dto)
    {
        var bill = await _context.Bills.FindAsync(id);
        if (bill is null)
        {
            return false;
        }

        bill.RecipientId = dto.RecipientId;
        bill.Description = dto.Description;
        bill.Amount = dto.Amount;
        bill.IsPaid = dto.IsPaid;
        bill.PaidDate = dto.PaidDate;

        await _context.SaveChangesAsync();
        return true;
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
        RecipientId = bill.RecipientId,
        RecipientFullName = bill.Recipient?.FullName,
        Description = bill.Description,
        Amount = bill.Amount,
        IsPaid = bill.IsPaid,
        PaidDate = bill.PaidDate,
        CreatedAt = bill.CreatedAt
    };
}
