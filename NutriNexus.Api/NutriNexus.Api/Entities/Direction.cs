using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class Direction
	{
		public int Id { get; set; }
		public int RecipeId { get; set; }
		public int StepNumber { get; set; }
		public string Description { get; set; } = null!;
	}
}