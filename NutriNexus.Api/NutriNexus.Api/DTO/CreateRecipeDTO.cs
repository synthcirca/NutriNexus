using NutriNexusAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.DTO;

//data annotations are things you can add to your properties to define what is expected of the properties
//this works with MVC but won't work with min apis
//we need endpoint filters
public record class CreateRecipeDTO(
    /*---- REQUIRED -----*/
    [Required][StringLength(50)] string Name,
    [Required] List<CreateRecipeIngredientDTO> Ingredients,
    [Required] List<string> Directions,

    /*---- OPTIONAL -----*/
    string ImgUrl,
    string Description,
    decimal Rating, 
    int PrepTime,
    int CookTime, 
    int TotalTime, 
    int ServingSize, 
    string Course, 
    string Cuisine, 
    List<Equipment> Equipment
);

public record class CreateRecipeIngredientDTO(
	[Required] string Name,
    [Required] float? Amount,
    string Unit
);
