using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class RecipeSummaryDTO(
	int Id,
	string Name,
	decimal Rating,
	string ImageUrl,
	int? PrepTime,
	int? CookTime,
	int? TotalTime,
	int ServingSize,
	string Description,
	ICollection<IngredientDTO> Ingredients,
	ICollection<DirectionDTO> Directions 
	//List<RecipeStepDTO> Steps
); 