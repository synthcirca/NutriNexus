using System;
using GameStore.API.Data;
using GameStore.API.DTO;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using GameStore.DTO;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

//extending the WebApplication class to include our endpoints
public static class GamesEndpoints
{
	const string GetGameEndpointName = "GetName";

	//return the type that you are extending
	public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
	{
		var group = app.MapGroup("games");

		group.MapGet("/", async (GameStoreContext dbContext) =>
			await dbContext.Games
				.Include(game => game.Genre) //if we don't do this each Genre property will be null
				.Select(game => game.ToGameSummaryDTO())
				.AsNoTracking() //improves performance by not tracking things in EF
				.ToListAsync() //just calling this allows the runtime to await the task
		);

		///////////////////////// GET /games/1
		group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
			{
				Game? game = await dbContext.Games.FindAsync(id);
				return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDTO());
			}
		)
			.WithName(GetGameEndpointName);

		/////////////////////// POST /games
		group.MapPost("/", async (CreateGameDTO newGame, GameStoreContext dbContext) =>
		{
			Game game = newGame.ToEntity();
			game.Genre = dbContext.Genres.Find(newGame.GenreId);

			dbContext.Games.Add(game);
			await dbContext.SaveChangesAsync(); //translates changes into SQL statements 

			return Results.CreatedAtRoute(
				GetGameEndpointName,
				new { id = game.Id },
				game.ToGameDetailsDTO());
		}).WithParameterValidation();

		//////////////////////// PUT /games
		group.MapPut("/{id}", async (int id, UpdateGameDTO updatedGame, GameStoreContext dbContext) =>
		{
			var existingGame = await dbContext.Games.FindAsync(id); 
			if (existingGame is null)
			{
				return Results.NotFound(); //could also just create the resource
										   //if you create though a database may just make up an ID
			}

			//locate existing entity inside of dbcontext and then update
			dbContext.Entry(existingGame)
				.CurrentValues
				.SetValues(updatedGame.ToEntity(id));

			await dbContext.SaveChangesAsync(); 
			return Results.NoContent();
		}).WithParameterValidation();

		////////////////////// DELETE games/1
		group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
		{
			//it doesn't matter if you don't find anything here, either way its not there any more
			await dbContext.Games
				.Where(game => game.Id == id)
				.ExecuteDeleteAsync(); //this is called match delete. efficient because you don't have to first find things in the api code
			return Results.NoContent();
		}
		);
	
		return group;
	}
}
