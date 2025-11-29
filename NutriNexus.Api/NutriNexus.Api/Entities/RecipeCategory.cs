using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class RecipeCategory
	{
		public long RecipeId { get; set; }
		public long CategoryId { get; set; }

		public Recipe Recipe { get; set; } = null!;
		public Category Category { get; set; } = null!;
	}
}