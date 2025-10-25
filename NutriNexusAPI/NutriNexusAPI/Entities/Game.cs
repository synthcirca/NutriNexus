using System;

namespace GameStore.API.Entities;

public class Game
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public int GenreId { get; set; } //why do we need the ID?
	public Genre? Genre { get; set; } //might only need the GenreID

	public decimal Price { get; set; }
	public DateOnly ReleaseDate { get; set; }
}

//there is a one-to-one relationship with Genre