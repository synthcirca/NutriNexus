using NutriNexusAPI.Entities;

namespace NutriNexusAPI.DTO;

public record class IngredientDTO(
	int Id,
	string Name,
	float Calories,
	int UnitId
); 