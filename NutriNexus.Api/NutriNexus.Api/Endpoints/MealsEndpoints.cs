using GameStore.API.Data;
using GameStore.API.DTO;
using Microsoft.EntityFrameworkCore;
using NutriNexusAPI.Data;
using NutriNexusAPI.DTO;
using NutriNexusAPI.Entities;
using NutriNexusAPI.Mapping;
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
                    .Include(recipe => recipe.RecipeSteps) //if we don't do this each Genre property will be null
                    .Select(recipe => recipe.ToRecipeSummaryDTO())
                    .AsNoTracking() //improves performance by not tracking things in EF
                    .ToListAsync() //just calling this allows the runtime to await the task
            );

            
            ///////////////////////// GET /games/1
            group.MapGet("/{id}", async (int id, MealAppContext dbContext) =>
            {
                Recipe? recipe = await dbContext.Recipes.FindAsync(id);
                return recipe is null ? Results.NotFound() : Results.Ok(recipe.ToRecipeSummaryDTO());
            }
            )
                .WithName(GetMealAppEndpointName);

            /////////////////////// POST /games
            group.MapPost("/", async (CreateRecipeDTO newRecipe, MealAppContext dbContext) =>
            {
                Recipe recipe = newRecipe.ToEntity();
                //game.Genre = dbContext.Genres.Find(newRecipe.GenreId);

                dbContext.Recipes.Add(recipe);
                await dbContext.SaveChangesAsync(); //translates changes into SQL statements 

                return Results.CreatedAtRoute(
                    GetMealAppEndpointName,
                    new { id = recipe.Id },
                    recipe.ToRecipeSummaryDTO());
            }).WithParameterValidation();

            //////////////////////// PUT /games
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
