using NutriNexusAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.DTO;

//data annotations are things you can add to your properties to define what is expected of the properties
//this works with MVC but won't work with min apis
//we need endpoint filters
public record class RecipeCreateRequest(
    /*---- REQUIRED -----*/
    [Required][StringLength(50)] string Name,
    [Required] List<RecipeIngredientCreateRequest> Ingredients,
    [Required] List<RecipeInstructionCreateRequest> Instructions,
    [Required] List<RecipeEquipmentCreateRequest> Equipment,

    /*---- OPTIONAL -----*/
    string ImgUrl,
    string Description,
    decimal Rating, 
    int PrepTime,
    int CookTime, 
    int TotalTime, 
    int ServingSize, 
    string Course, 
    string Cusine,
    string Author,
    string SourceUrl,
    string VideoUrl,
    string Notes
);

public record class RecipeEquipmentCreateRequest(
    string Name, 
    string Description,
    string SourceUrl,
    int Quantity,
    string Notes
);

public record class RecipeInstructionCreateRequest(
    [Required] int StepNumber,
    [Required] string Instruction
);
