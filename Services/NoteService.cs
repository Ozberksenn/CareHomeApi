using CareHomeApi.Data;
using CareHomeApi.DTOs.Note;
using CareHomeApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareHomeApi.Services;

public class NoteService : INoteService
{
    private readonly AppDbContext _context;

    public NoteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NoteDto>> GetAllAsync()
    {
        return await _context.Notes
            .Include(n => n.Recipient)
            .Include(n => n.Provider)
            .Select(n => ToDto(n))
            .ToListAsync();
    }

    public async Task<NoteDto?> GetByIdAsync(int id)
    {
        var note = await _context.Notes
            .Include(n => n.Recipient)
            .Include(n => n.Provider)
            .FirstOrDefaultAsync(n => n.Id == id);

        return note is null ? null : ToDto(note);
    }

    public async Task<NoteDto> CreateAsync(CreateNoteDto dto)
    {
        var note = new Models.Note
        {
            RecipientId = dto.RecipientId,
            ProviderId = dto.ProviderId,
            Text = dto.Text,
            IsPaid = dto.IsPaid
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        if (note.RecipientId is not null)
        {
            await _context.Entry(note).Reference(n => n.Recipient).LoadAsync();
        }
        if (note.ProviderId is not null)
        {
            await _context.Entry(note).Reference(n => n.Provider).LoadAsync();
        }

        return ToDto(note);
    }

    public async Task<bool> UpdateAsync(int id, UpdateNoteDto dto)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note is null)
        {
            return false;
        }

        note.Text = dto.Text;
        note.IsPaid = dto.IsPaid;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note is null)
        {
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }

    private static NoteDto ToDto(Models.Note note) => new()
    {
        Id = note.Id,
        RecipientId = note.RecipientId,
        RecipientFullName = note.Recipient?.FullName,
        ProviderId = note.ProviderId,
        ProviderFullName = note.Provider?.FullName,
        Text = note.Text,
        IsPaid = note.IsPaid,
        CreatedAt = note.CreatedAt
    };
}
