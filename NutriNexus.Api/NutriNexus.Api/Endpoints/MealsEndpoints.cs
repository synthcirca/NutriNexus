using Microsoft.EntityFrameworkCore;
using NutriNexus.Api.DTO;
using NutriNexus.Api.Entities;
using NutriNexusAPI.Data;
using NutriNexusAPI.DTO;
using NutriNexusAPI.Entities;
using NutriNexusAPI.Mapping;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace NutriNexusAPI.Endpoints
{
    public static class MealsEndpoints
    {
        const string GetMealAppEndpointName = "GetName";

        //return the type that you are extending
        public static RouteGroupBuilder MapMealAppEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("meals");
            var hello = app.MapGroup("hello");

            hello.MapGet("/", () =>
            {
                return "Hello, world!";
            });


            group.MapGet("/", async (MealAppContext dbContext) =>
                await dbContext.Recipes
                    .Include(recipe => recipe.RecipeIngredients) //if we don't do this each 
                    .Include(recipe => recipe.RecipeInstructions) 
                    .Select(recipe => recipe.ToRecipeSummaryDTO())
                    .AsNoTracking() //improves performance by not tracking things in EF
                    .ToListAsync() //just calling this allows the runtime to await the task
            );


            ///////////////////////// GET /meals/1
            group.MapGet("/{id}", async (int id, MealAppContext dbContext) =>
            {
                Recipe? recipe = await dbContext.Recipes
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.RecipeEquipment)
                        .ThenInclude(re => re.Equipment)
                    .Include(r => r.RecipeInstructions)
                    .FirstOrDefaultAsync(r => r.RecipeId == id);
               
                return recipe is null ? Results.NotFound() : Results.Ok(recipe.ToRecipeDetailDTO());
            }
            )
                .WithName(GetMealAppEndpointName);


            /////////////////////// POST /meals
            group.MapPost("/", async (RecipeCreateRequest newRecipe, MealAppContext dbContext) =>
            {
                Recipe recipe = await newRecipe.ToEntityAsync(dbContext);
                
                dbContext.Recipes.Add(recipe);
                await dbContext.SaveChangesAsync(); //translates changes into SQL statements 

                return Results.CreatedAtRoute(
                    GetMealAppEndpointName,
                    new { id = recipe.RecipeId },
                    recipe.ToRecipeDetailDTO());

            }).WithParameterValidation();

            //////////////////////// PUT /recipes
            group.MapPut("/{id}", async (int id, RecipeUpdateRequest request, MealAppContext dbContext) =>
            {
                // Load existing recipe with all related data
                var existingRecipe = await dbContext.Recipes
                    .Include(r => r.RecipeIngredients)
                    .Include(r => r.RecipeEquipment)
                    .Include(r => r.RecipeInstructions)
                    .FirstOrDefaultAsync(r => r.RecipeId == id);

                if (existingRecipe is null)
                    return Results.NotFound();

                // Update basic properties
                existingRecipe.Name = request.Name;
                existingRecipe.ImageUrl = request.ImageUrl;
                existingRecipe.Description = request.Description;
                existingRecipe.Rating = request.Rating;
                existingRecipe.PrepTime = request.PrepTime;
                existingRecipe.CookTime = request.CookTime;
                existingRecipe.TotalTime = request.TotalTime;
                existingRecipe.ServingSize = request.ServingSize;
                existingRecipe.Course = request.Course;
                existingRecipe.Cuisine = request.Cuisine;

                // Remove existing instructions

                dbContext.RecipeInstructions.RemoveRange(existingRecipe.RecipeInstructions);


                existingRecipe.RecipeInstructions = request.RecipeInstructions?.Select((i) => new RecipeInstruction
                {
                    StepNumber = i.StepNumber,
                    Instruction = i.Instruction
                }).ToList() ?? new List<RecipeInstruction>();
                //existingRecipe.Author = request.Author;
                //existingRecipe.SourceUrl = request.SourceUrl;
                //existingRecipe.VideoUrl = request.VideoUrl;
                //existingRecipe.Notes = request.Notes;

                // Remove existing ingredients
                dbContext.RecipeIngredients.RemoveRange(existingRecipe.RecipeIngredients);

                // Add new ingredients
                foreach (var ingredientRequest in request.RecipeIngredients ?? new List<RecipeIngredientCreateRequest>())
                {
                    var ingredient = await dbContext.Ingredients
                        .FirstOrDefaultAsync(i => i.Name.ToLower() == ingredientRequest.Name.ToLower());

                    if (ingredient == null)
                    {
                        ingredient = new Ingredient
                        {
                            Name = ingredientRequest.Name,
                            Calories = null
                        };
                        dbContext.Ingredients.Add(ingredient);
                    }

                    existingRecipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        Ingredient = ingredient,
                        Quantity = ingredientRequest.Quantity,
                        Unit = ingredientRequest.Unit,
                        Note = ingredientRequest.Note
                    });
                }

                // Remove existing equipment
                dbContext.RecipeEquipment.RemoveRange(existingRecipe.RecipeEquipment);

                // Add new equipment
                foreach (var equipmentRequest in request.RecipeEquipment ?? new List<RecipeEquipmentCreateRequest>())
                {
                    var equipment = await dbContext.Equipment
                        .FirstOrDefaultAsync(e => e.Name.ToLower() == equipmentRequest.Name.ToLower());

                    if (equipment == null)
                    {
                        equipment = new Equipment
                        {
                            Name = equipmentRequest.Name,
                            Description = equipmentRequest.Description,
                            SourceUrl = equipmentRequest.SourceUrl
                        };
                        dbContext.Equipment.Add(equipment);
                    }

                    existingRecipe.RecipeEquipment.Add(new RecipeEquipment
                    {
                        RecipeId = existingRecipe.RecipeId,
                        Recipe = existingRecipe,
                        EquipmentId = equipment.Id,
                        Equipment = equipment,
                        Quantity = equipmentRequest.Quantity,
                        Notes = equipmentRequest.Notes
                    });
                }

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingRecipe.ToRecipeDetailDTO());
            })
            .WithName("UpdateRecipe")
            .WithOpenApi();

            group.MapDelete("/{id}", async (int id, MealAppContext dbContext) =>
            {
                var recipe = await dbContext.Recipes
                    .Include(r => r.RecipeIngredients)
                    .Include(r => r.RecipeEquipment)
                    .Include(r => r.RecipeInstructions)
                    .FirstOrDefaultAsync(r => r.RecipeId == id);

                if (recipe is null)
                    return Results.NotFound();

                dbContext.Recipes.Remove(recipe);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("DeleteRecipe")
            .WithOpenApi();
            return group;
        }
    }
}
