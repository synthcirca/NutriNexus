using System.ComponentModel.DataAnnotations;

namespace GameStore.API.DTO;

//data annotations are things you can add to your properties to define what is expected of the properties
//this works with MVC but won't work with min apis
//we need endpoint filters
public record class CreateGameDTO(
	[Required][StringLength(50)] string Name,
	int GenreId,
	[Range(1, 100)] decimal Price,
	DateOnly ReleaseDate
	); 