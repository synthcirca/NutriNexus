using System;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
	public DbSet<Game> Games => Set<Game>(); //DbSet is an object that can be used to query the db
	public DbSet<Genre> Genres => Set<Genre>();

	//this will execute when you do the model migration
	//only for simple static data that wont be changed
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Genre>().HasData(
			new { Id = 1, Name = "Fighting" },
			new { Id = 2, Name = "Roleplaying" },
			new { Id = 3, Name = "Sports" },
			new { Id = 4, Name = "Racing" },
			new { Id = 5, Name = "Kids and Family" }
		);
	}
}
