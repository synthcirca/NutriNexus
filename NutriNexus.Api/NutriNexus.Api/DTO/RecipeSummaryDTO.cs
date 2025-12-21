using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class RecipeSummaryDTO(
	int Id,
	string Name,
    string ImageUrl,
    string Description,
    int TotalTime,
    decimal Rating
); 