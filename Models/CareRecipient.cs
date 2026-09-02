namespace CareHomeApi.Models;

public class CareRecipient
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
