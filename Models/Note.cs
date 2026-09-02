namespace CareHomeApi.Models;

public class Note
{
    public int Id { get; set; }
    public int? RecipientId { get; set; }
    public CareRecipient? Recipient { get; set; }
    public int? ProviderId { get; set; }
    public CareProvider? Provider { get; set; }
    public string? Text { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
