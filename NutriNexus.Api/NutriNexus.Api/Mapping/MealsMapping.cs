using Microsoft.EntityFrameworkCore;
using NutriNexus.Api.DTO;
using NutriNexus.Api.Entities;
using NutriNexusAPI.Data;
using NutriNexusAPI.DTO;
using NutriNexusAPI.Entities;
using System.Diagnostics;

namespace NutriNexusAPI.Mapping
{
    public static class MealsMapping
    {

        public static async Task<Recipe> ToEntityAsync(this RecipeCreateRequest newRecipe, MealAppContext db)
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
                Cuisine = newRecipe.Cusine,

                RecipeInstructions = newRecipe.Instructions?.Select((i) => new RecipeInstruction
                {
                    StepNumber = i.StepNumber,
                    Instruction = i.Instruction
                }).ToList() ?? new List<RecipeInstruction>()
            };

            // Process ingredients
            foreach (var ingredientRequest in newRecipe.Ingredients ?? new List<RecipeIngredientCreateRequest>())
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
                    Recipe = recipe,
                    Ingredient = ingredient,
                    Quantity = ingredientRequest.Quantity,
                    Unit = ingredientRequest.Unit,
                    Note = ingredientRequest.Note
                });
            }

            return recipe;
        }

        //you only need to do a library if you have a TON of properties that you need to map
        public static RecipeSummaryResponse ToRecipeSummaryDTO(this Recipe recipe)
        {

            return new RecipeSummaryResponse
            (
                recipe.RecipeId,
                recipe.Name,
                recipe.ImageUrl,
                recipe.Description,
                recipe.TotalTime,
                recipe.Rating
            );
        }

        public static RecipeDetailResponse ToRecipeDetailDTO(this Recipe recipe)
        {
            Debug.WriteLine($"First ingredient: {recipe.RecipeIngredients.First<RecipeIngredient>().Ingredient.Name}");

            var ingredientsList = recipe.RecipeIngredients?
                .OrderBy(ri => ri.Id)
                .Select(ri => new RecipeIngredientResponse
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit,
                    Calories = ri.Ingredient.Calories
                })
                .ToList() ?? new List<RecipeIngredientResponse>();

            Debug.WriteLine($"First Ingredient again: {ingredientsList.First().Name}");
            var resp =  new RecipeDetailResponse
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                ImageUrl = recipe.ImageUrl,
                Description = recipe.Description,
                Rating = recipe.Rating,
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,
                TotalTime = recipe.TotalTime,
                ServingSize = recipe.ServingSize,
                Course = recipe.Course,
                Cuisine = recipe.Cuisine,
                Ingredients = recipe.RecipeIngredients?
                .OrderBy(ri => ri.Id)
                .Select(ri => new RecipeIngredientResponse
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit,
                    Calories = ri.Ingredient.Calories
                })
                .ToList() ?? new List<RecipeIngredientResponse>(),
                RecipeInstructions = recipe.RecipeInstructions?
                .OrderBy(d => d.StepNumber)
                .Select(d => new RecipeInstructionResponse
                {
                    StepNumber = d.StepNumber,
                    Instruction = d.Instruction
                })
                .ToList() ?? new List<RecipeInstructionResponse>(),
                Equipment = recipe.RecipeEquipment?
                .OrderBy(d => d.Id)
                .Select(d => new RecipeEquipmentResponse
                {
                    Name = d.Equipment.Name,
                    ImageUrl = d.Equipment.ImageUrl,
                })
                .ToList() ?? new List<RecipeEquipmentResponse>()
            };

            return resp; 
        }
    }
}
