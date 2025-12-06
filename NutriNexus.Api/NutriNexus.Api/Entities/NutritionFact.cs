using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class NutritionFact
	{
		public int Id { get; set; }
		public int RecipeId { get; set; }
		public decimal? Value { get; set; }
		public int NutritionFactCategoryId { get; set; }
	}
}