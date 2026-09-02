namespace CareHomeApi.DTOs.CareProvider;

public class CareProviderDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public decimal? MonthlySalary { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCareProviderDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public decimal? MonthlySalary { get; set; }
}

public class UpdateCareProviderDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public decimal? MonthlySalary { get; set; }
}
