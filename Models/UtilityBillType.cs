namespace CareHomeApi.Models;

public class UtilityBillType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
