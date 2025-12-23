using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.DTO;

public record class RecipeIngredientCreateRequest(
	[Required] string Name,
    [Required] float? Quantity,
    string Unit,
    string Note
);

