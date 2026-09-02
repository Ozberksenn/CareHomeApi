using CareHomeApi.DTOs.Note;

namespace CareHomeApi.Services.Interfaces;

public interface INoteService
{
    Task<List<NoteDto>> GetAllAsync();
    Task<NoteDto?> GetByIdAsync(int id);
    Task<NoteDto> CreateAsync(CreateNoteDto dto);
    Task<bool> UpdateAsync(int id, UpdateNoteDto dto);
    Task<bool> DeleteAsync(int id);
}
