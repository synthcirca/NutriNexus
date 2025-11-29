using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class Direction
	{
		public long DirectionId { get; set; }
		public long RecipeId { get; set; }
		public int StepNumber { get; set; }
		public string Instruction { get; set; } = null!;

		public Recipe Recipe { get; set; } = null!;
	}
}