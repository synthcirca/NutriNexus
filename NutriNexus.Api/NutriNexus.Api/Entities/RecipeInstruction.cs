using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.Entities
{
	public class RecipeInstruction
	{
		[Key]
		public Guid RecipeInstructionId { get; set; }	

		public Guid RecipeId { get; set; }

		public int StepNumber { get; set; }
		public string Instruction { get; set; } = null!;
	}
}