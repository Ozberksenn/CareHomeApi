namespace CareHomeApi.Models;

public class Bill
{
    public int Id { get; set; }
    public int? RecipientId { get; set; }
    public CareRecipient? Recipient { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly PaidDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
