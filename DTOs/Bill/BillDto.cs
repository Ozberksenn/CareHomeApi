namespace CareHomeApi.DTOs.Bill;

public class BillDto
{
    public int Id { get; set; }
    public int UtilityBillTypeId { get; set; }
    public string? UtilityBillTypeName { get; set; }
    public decimal Amount { get; set; }
    public DateOnly BillDate { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly? PaidDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateBillDto
{
    public int UtilityBillTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly? BillDate { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly? PaidDate { get; set; }
}

public class UpdateBillDto
{
    public int UtilityBillTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly BillDate { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly? PaidDate { get; set; }
}

public class MonthlyBillsDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BillDto> Bills { get; set; } = new();
}
