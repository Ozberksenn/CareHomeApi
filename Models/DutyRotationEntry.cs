namespace CareHomeApi.Models;

public class DutyRotationEntry
{
    public int Id { get; set; }
    public int CareProviderId { get; set; }
    public CareProvider? CareProvider { get; set; }
    public int Order { get; set; }
}
