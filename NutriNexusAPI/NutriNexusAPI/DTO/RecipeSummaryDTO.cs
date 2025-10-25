using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class RecipeSummaryDTO(
	int Id,
	string Name,
	int TimeEstimate,
	int ServingSize,
	List<RecipeStepDTO> Steps
	); 