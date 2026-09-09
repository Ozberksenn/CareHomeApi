namespace CareHomeApi.Models;

public class Meal
{
    public int Id { get; set; }
    public int? ProviderId { get; set; }
    public CareProvider? Provider { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
