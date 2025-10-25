using System;
using GameStore.API.DTO;
using GameStore.API.Entities;
using GameStore.DTO;

namespace GameStore.API.Mapping;

public static class GameMapping
{
	public static Game ToEntity(this CreateGameDTO game)
	{
		return new Game()
		{
			Name = game.Name,
			GenreId = game.GenreId,
			Price = game.Price,
			ReleaseDate = game.ReleaseDate
		};
	}

	//want to create this with an ID because the user is trying to create a new game
	public static Game ToEntity(this UpdateGameDTO game, int id)
	{
		return new Game()
		{
			Id = id, 
			Name = game.Name,
			GenreId = game.GenreId,
			Price = game.Price,
			ReleaseDate = game.ReleaseDate
		};
	}

	//you only need to do a library if you have a TON of properties that you need to map
	public static GameSummaryDTO ToGameSummaryDTO(this Game game)
	{
		return new GameSummaryDTO(
			game.Id,
			game.Name,
			game.Genre!.Name,
			game.Price,
			game.ReleaseDate
		);
	}

	public static GameDetailsDTO ToGameDetailsDTO(this Game game)
	{
		return new GameDetailsDTO(
			game.Id,
			game.Name,
			game.GenreId,
			game.Price,
			game.ReleaseDate
		);
	}
}
