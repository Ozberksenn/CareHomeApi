namespace CareHomeApi.DTOs.UtilityBillType;

public class UtilityBillTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateUtilityBillTypeDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateUtilityBillTypeDto
{
    public string Name { get; set; } = string.Empty;
}
