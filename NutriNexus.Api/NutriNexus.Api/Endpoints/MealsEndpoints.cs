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
                    recipe.ToRecipeSummaryDTO());

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
                //existingRecipe.Author = request.Author;
                //existingRecipe.SourceUrl = request.SourceUrl;
                //existingRecipe.VideoUrl = request.VideoUrl;
                //existingRecipe.Notes = request.Notes;

                // Remove existing ingredients
                dbContext.RecipeIngredients.RemoveRange(existingRecipe.RecipeIngredients);

                // Add new ingredients
                foreach (var ingredientRequest in request.Ingredients ?? new List<RecipeIngredientRequest>())
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
                        Quantity = ingredientRequest.Amount,
                        Unit = ingredientRequest.Unit,
                        Note = ingredientRequest.Note
                    });
                }

                // Remove existing equipment
                dbContext.RecipeEquipment.RemoveRange(existingRecipe.RecipeEquipment);

                // Add new equipment
                foreach (var equipmentRequest in request.Equipment ?? new List<RecipeEquipmentRequest>())
                {
                    var equipment = await dbContext.Equipment
                        .FirstOrDefaultAsync(e => e.Name.ToLower() == equipmentRequest.Name.ToLower());

                    if (equipment == null)
                    {
                        equipment = new Equipment
                        {
                            Name = equipmentRequest.Name
                        };
                        dbContext.Equipment.Add(equipment);
                    }

                    existingRecipe.RecipeEquipment.Add(new RecipeEquipment
                    {
                        Equipment = equipment,
                        Quantity = equipmentRequest.Quantity,
                        Notes = equipmentRequest.Notes
                    });
                }

                // Remove existing instructions
                dbContext.RecipeInstructions.RemoveRange(existingRecipe.RecipeInstructions);

                // Add new instructions
                if (request.Directions != null)
                {
                    for (int i = 0; i < request.Directions.Count; i++)
                    {
                        existingRecipe.RecipeInstructions.Add(new RecipeInstruction
                        {
                            StepNumber = i + 1,
                            Instruction = request.Directions[i]
                        });
                    }
                }

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingRecipe.ToRecipeDetailDTO());
            })
            .WithName("UpdateRecipe")
            .WithOpenApi();


            // group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
            // {
            //     var existingGame = await dbContext.Games.FindAsync(id);
            //     if (existingGame is null)
            //     {
            //         return Results.NotFound(); //could also just create the resource
            //                                    //if you create though a database may just make up an ID
            //     }

            //     //locate existing entity inside of dbcontext and then update
            //     dbContext.Entry(existingGame)
            //         .CurrentValues
            //         .SetValues(updatedGame.ToEntity(id));

            //     await dbContext.SaveChangesAsync();
            //     return Results.NoContent();
            // }).WithParameterValidation();

            // ////////////////////// DELETE games/1
            // group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            // {
            //     //it doesn't matter if you don't find anything here, either way its not there any more
            //     await dbContext.Games
            //         .Where(game => game.Id == id)
            //         .ExecuteDeleteAsync(); //this is called match delete. efficient because you don't have to first find things in the api code
            //     return Results.NoContent();
            // }
            // );


            return group;
        }
    }
}
