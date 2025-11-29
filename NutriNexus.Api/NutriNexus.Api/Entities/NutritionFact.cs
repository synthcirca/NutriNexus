using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class NutritionFact
	{
		public long NutritionFactId { get; set; }
		public long RecipeId { get; set; }
		public string Name { get; set; } = null!;
		public decimal? Amount { get; set; }
		public string? Unit { get; set; }

		public Recipe Recipe { get; set; } = null!;
	}
}