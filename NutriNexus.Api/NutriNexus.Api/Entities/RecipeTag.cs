using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class RecipeTag
	{
		public long RecipeId { get; set; }
		public long TagId { get; set; }

		public Recipe Recipe { get; set; } = null!;
		public Tag Tag { get; set; } = null!;
	}
}