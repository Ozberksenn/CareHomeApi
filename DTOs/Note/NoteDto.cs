namespace CareHomeApi.DTOs.Note;

public class NoteDto
{
    public int Id { get; set; }
    public int? RecipientId { get; set; }
    public string? RecipientFullName { get; set; }
    public int? ProviderId { get; set; }
    public string? ProviderFullName { get; set; }
    public string? Text { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateNoteDto
{
    public int? RecipientId { get; set; }
    public int? ProviderId { get; set; }
    public string? Text { get; set; }
    public bool IsPaid { get; set; }
}

public class UpdateNoteDto
{
    public string? Text { get; set; }
    public bool IsPaid { get; set; }
}
