namespace CareHomeApi.DTOs.CareRecipient;

public class CareRecipientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCareRecipientDto
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
}

public class UpdateCareRecipientDto
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
}
