using System;

namespace GameStore.API.Entities;

public class Genre
{
	public int Id { get; set; }	
	public required String Name { get; set; } //required keywords means that an error will be thrown if this isn't defined
}
