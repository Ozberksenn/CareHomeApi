namespace CareHomeApi.DTOs.Bill;

public class BillDto
{
    public int Id { get; set; }
    public int? RecipientId { get; set; }
    public string? RecipientFullName { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly PaidDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBillDto
{
    public int? RecipientId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly? PaidDate { get; set; }
}

public class UpdateBillDto
{
    public int? RecipientId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly PaidDate { get; set; }
}
