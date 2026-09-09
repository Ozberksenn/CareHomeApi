namespace CareHomeApi.Models;

public class Bill
{
    public int Id { get; set; }
    public int UtilityBillTypeId { get; set; }
    public UtilityBillType? UtilityBillType { get; set; }
    public decimal Amount { get; set; }
    public DateOnly PaidDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
