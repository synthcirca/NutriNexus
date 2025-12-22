using Microsoft.EntityFrameworkCore;
using NutriNexus.Api.Entities;
using NutriNexusAPI.Data;
using NutriNexusAPI.DTO;
using NutriNexusAPI.Entities;

namespace NutriNexusAPI.Mapping
{
    public static class MealsMapping
    {
        // public static Game ToEntity(this CreateGameDTO game)
        // {
        //     return new Game()
        //     {
        //         Name = game.Name,
        //         GenreId = game.GenreId,
        //         Price = game.Price,
        //         ReleaseDate = game.ReleaseDate
        //     };
        // }

        public static async Task<Recipe> ToEntityAsync(this CreateRecipeDTO newRecipe, MealAppContext db)
        {
            var recipe =  new Recipe()
            {
                Name = newRecipe.Name,
                ImageUrl = newRecipe.ImgUrl != null ? newRecipe.ImgUrl : "genericImg",
                Description = newRecipe.Description,
                Rating = newRecipe.Rating,
                PrepTime = newRecipe.PrepTime,
                CookTime = newRecipe.CookTime,
                TotalTime = newRecipe.TotalTime,
                ServingSize = newRecipe.ServingSize, 
                Course = newRecipe.Course,
                Cuisine = newRecipe.Cuisine,

                Directions = newRecipe.Directions?.Select((step, index) => new Direction
                {
                    StepNumber = index + 1,
                    Instruction = step
                }).ToList() ?? new List<Direction>()
            };

            // Process ingredients
            foreach (var ingredientRequest in newRecipe.Ingredients ?? new List<CreateRecipeIngredientDTO>())
            {
                // Try to find existing ingredient by name (case-insensitive)
                var ingredient = await db.Ingredients
                    .FirstOrDefaultAsync(i => i.Name.ToLower() == ingredientRequest.Name.ToLower());

                // If ingredient doesn't exist, create it
                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        Name = ingredientRequest.Name,
                        Calories = null // To be populated later
                    };
                    db.Ingredients.Add(ingredient);
                }

                // Create the recipe-ingredient relationship
                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Amount = ingredientRequest.Amount,
                    Unit = ingredientRequest.Unit
                });
            }

            return recipe;
        }

        // //want to create this with an ID because the user is trying to create a new game
        // public static Game ToEntity(this UpdateGameDTO game, int id)
        // {
        //     return new Game()
        //     {
        //         Id = id, 
        //         Name = game.Name,
        //         GenreId = game.GenreId,
        //         Price = game.Price,
        //         ReleaseDate = game.ReleaseDate
        //     };
        // }

        //you only need to do a library if you have a TON of properties that you need to map
        public static RecipeSummaryDTO ToRecipeSummaryDTO(this Recipe recipe)
        {

            return new RecipeSummaryDTO
            (
                recipe.RecipeId,
                recipe.Name,
                recipe.ImageUrl,
                recipe.Description,
                recipe.TotalTime,
                recipe.Rating
            );
        }

        //public static RecipeSummaryDTO ToRecipeSummaryDTO(this Recipe recipe)
        //{

        //    return new RecipeSummaryDTO
        //    (
        //        recipe.Id,
        //        recipe.Name,
        //        recipe.Rating,
        //        recipe.ImageUrl,
        //        recipe.PrepTime,
        //        recipe.CookTime,
        //        recipe.TotalTime,
        //        recipe.ServingSize,
        //        recipe.Description,
        //        //recipe.Ingredients
        //        recipe.Ingredients?.Select(i => new IngredientDTO
        //        (
        //            i.Id,
        //            i.Name,
        //            i.Calories,
        //            i.UnitId,
        //            i.RecipeId
        //        )).ToList() ?? new List<IngredientDTO>(),
        //        recipe.Directions?.Select(i => new DirectionDTO
        //        (
        //            i.Id,
        //            i.StepNumber,
        //            i.Description,
        //            i.RecipeId
        //        )).ToList() ?? new List<DirectionDTO>()
        //    );
        //}

    }
}
