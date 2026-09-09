namespace CareHomeApi.DTOs.DutyRotation;

public class DutyRotationEntryDto
{
    public int Id { get; set; }
    public int CareProviderId { get; set; }
    public string ProviderFullName { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class CreateDutyRotationEntryDto
{
    public int CareProviderId { get; set; }
}

public class ReorderDutyRotationDto
{
    public List<int> CareProviderIds { get; set; } = new();
}

public class SwapDutyRotationDto
{
    public int CareProviderId1 { get; set; }
    public int CareProviderId2 { get; set; }
}

public class CurrentDutyDto
{
    public int CareProviderId { get; set; }
    public string ProviderFullName { get; set; } = string.Empty;
    public DateOnly WeekStart { get; set; }
    public DateOnly WeekEnd { get; set; }
}
