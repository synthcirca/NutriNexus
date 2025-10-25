using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.DTO;

//data annotations are things you can add to your properties to define what is expected of the properties
//this works with MVC but won't work with min apis
//we need endpoint filters
public record class CreateRecipeDTO(
	[Required][StringLength(50)] string Name,
	List<CreateRecipeStepDTO> Steps
); 

public record class CreateRecipeStepDTO(
	[Required] string Description
);