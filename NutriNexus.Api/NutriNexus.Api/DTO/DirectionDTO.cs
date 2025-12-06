using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class DirectionDTO(
	int Id,
	int StepNumber,
	string Description,
	int RecipeId
); 