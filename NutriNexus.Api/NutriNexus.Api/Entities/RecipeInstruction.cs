using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class RecipeInstruction
	{
		public int Id { get; set; }	

		public int RecipeId { get; set; }

		public int StepNumber { get; set; }
		public string Instruction { get; set; } = null!;
	}
}