namespace CareHomeApi.Models;

public class CareProvider
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    public ICollection<DutyRotationEntry> DutyRotationEntries { get; set; } = new List<DutyRotationEntry>();
}
