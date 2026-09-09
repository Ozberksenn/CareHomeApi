namespace CareHomeApi.DTOs.Meal;

public class MealDto
{
    public int Id { get; set; }
    public int? ProviderId { get; set; }
    public string? ProviderFullName { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateMealDto
{
    public int? ProviderId { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class UpdateMealDto
{
    public DateOnly Date { get; set; }
    public string Description { get; set; } = string.Empty;
}
