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

        public static Recipe ToEntity(this CreateRecipeDTO recipe)
		{
            return new Recipe()
            {
                Name = recipe.Name,
                RecipeSteps = recipe.Steps.Select(i => new RecipeStep
				{
					Description = i.Description
				}).ToList()
            };
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
                recipe.Id,
                recipe.Name,
                recipe.TimeEstimate,
                recipe.ServingSize,
                recipe.RecipeSteps?.Select(i => new RecipeStepDTO
                (
                    i.Id,
                    i.Description
                )).ToList() ?? new List<RecipeStepDTO>()
            );
         
        }

        // public static GameDetailsDTO ToGameDetailsDTO(this Game game)
        // {
        //     return new GameDetailsDTO(
        //         game.Id,
        //         game.Name,
        //         game.GenreId,
        //         game.Price,
        //         game.ReleaseDate
        //     );
        // }
	}
}
