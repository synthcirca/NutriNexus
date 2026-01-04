using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class RecipeSummaryResponse(
	Guid Id,
	string Name,
    string ImageUrl,
    string Description,
    TimeSpan TotalTime,
    decimal Rating
); 