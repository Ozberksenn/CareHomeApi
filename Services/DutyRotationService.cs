using CareHomeApi.Data;
using CareHomeApi.DTOs.DutyRotation;
using CareHomeApi.Models;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class DutyRotationService : IDutyRotationService
{
    // A Monday, used purely as a fixed reference point for computing week offsets.
    private static readonly DateOnly RotationAnchor = new(2024, 1, 1);

    private readonly AppDbContext _context;

    public DutyRotationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DutyRotationEntryDto>> GetAllAsync()
    {
        return await _context.DutyRotationEntries
            .Include(d => d.CareProvider)
            .OrderBy(d => d.Order)
            .Select(d => ToDto(d))
            .ToListAsync();
    }

    public async Task<(AddDutyRotationResult Result, DutyRotationEntryDto? Entry)> AddAsync(CreateDutyRotationEntryDto dto)
    {
        var providerExists = await _context.CareProviders.AnyAsync(p => p.Id == dto.CareProviderId);
        if (!providerExists)
        {
            return (AddDutyRotationResult.ProviderNotFound, null);
        }

        var alreadyInRotation = await _context.DutyRotationEntries
            .AnyAsync(d => d.CareProviderId == dto.CareProviderId);
        if (alreadyInRotation)
        {
            return (AddDutyRotationResult.AlreadyInRotation, null);
        }

        var nextOrder = await _context.DutyRotationEntries.AnyAsync()
            ? await _context.DutyRotationEntries.MaxAsync(d => d.Order) + 1
            : 0;

        var entry = new DutyRotationEntry
        {
            CareProviderId = dto.CareProviderId,
            Order = nextOrder
        };

        _context.DutyRotationEntries.Add(entry);
        await _context.SaveChangesAsync();

        await _context.Entry(entry).Reference(d => d.CareProvider).LoadAsync();
        return (AddDutyRotationResult.Created, ToDto(entry));
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entry = await _context.DutyRotationEntries.FindAsync(id);
        if (entry is null)
        {
            return false;
        }

        _context.DutyRotationEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReorderAsync(ReorderDutyRotationDto dto)
    {
        var entries = await _context.DutyRotationEntries.ToListAsync();

        var currentProviderIds = entries.Select(e => e.CareProviderId).ToHashSet();
        if (dto.CareProviderIds.Count != entries.Count || !currentProviderIds.SetEquals(dto.CareProviderIds))
        {
            return false;
        }

        for (var i = 0; i < dto.CareProviderIds.Count; i++)
        {
            var entry = entries.First(e => e.CareProviderId == dto.CareProviderIds[i]);
            entry.Order = i;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SwapAsync(SwapDutyRotationDto dto)
    {
        var entry1 = await _context.DutyRotationEntries
            .FirstOrDefaultAsync(d => d.CareProviderId == dto.CareProviderId1);
        var entry2 = await _context.DutyRotationEntries
            .FirstOrDefaultAsync(d => d.CareProviderId == dto.CareProviderId2);

        if (entry1 is null || entry2 is null)
        {
            return false;
        }

        (entry1.Order, entry2.Order) = (entry2.Order, entry1.Order);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CurrentDutyDto?> GetCurrentAsync(DateOnly? date)
    {
        var entries = await _context.DutyRotationEntries
            .Include(d => d.CareProvider)
            .OrderBy(d => d.Order)
            .ToListAsync();

        if (entries.Count == 0)
        {
            return null;
        }

        var weekStart = WeekStart(date ?? DateOnly.FromDateTime(DateTime.UtcNow));
        var entry = entries[WeekIndex(weekStart, entries.Count)];

        return ToCurrentDutyDto(entry, weekStart);
    }

    public async Task<List<CurrentDutyDto>> GetUpcomingAsync(int weeks)
    {
        var entries = await _context.DutyRotationEntries
            .Include(d => d.CareProvider)
            .OrderBy(d => d.Order)
            .ToListAsync();

        if (entries.Count == 0 || weeks <= 0)
        {
            return new List<CurrentDutyDto>();
        }

        var firstWeekStart = WeekStart(DateOnly.FromDateTime(DateTime.UtcNow));
        var result = new List<CurrentDutyDto>(weeks);

        for (var i = 0; i < weeks; i++)
        {
            var weekStart = firstWeekStart.AddDays(7 * i);
            var entry = entries[WeekIndex(weekStart, entries.Count)];
            result.Add(ToCurrentDutyDto(entry, weekStart));
        }

        return result;
    }

    private static DateOnly WeekStart(DateOnly date)
    {
        // Duty weeks run Tuesday -> Monday (shifted one day later than a calendar week).
        var daysSinceWeekStart = ((int)date.DayOfWeek + 5) % 7;
        return date.AddDays(-daysSinceWeekStart);
    }

    private static int WeekIndex(DateOnly weekStart, int rotationCount)
    {
        var weeksSinceAnchor = (weekStart.DayNumber - WeekStart(RotationAnchor).DayNumber) / 7;
        var index = weeksSinceAnchor % rotationCount;
        return index < 0 ? index + rotationCount : index;
    }

    private static CurrentDutyDto ToCurrentDutyDto(DutyRotationEntry entry, DateOnly weekStart) => new()
    {
        CareProviderId = entry.CareProviderId,
        ProviderFullName = entry.CareProvider!.FullName,
        WeekStart = weekStart,
        WeekEnd = weekStart.AddDays(6)
    };

    private static DutyRotationEntryDto ToDto(DutyRotationEntry entry) => new()
    {
        Id = entry.Id,
        CareProviderId = entry.CareProviderId,
        ProviderFullName = entry.CareProvider?.FullName ?? string.Empty,
        Order = entry.Order
    };
}
