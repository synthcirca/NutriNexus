using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class Tag
	{
		public long TagId { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
	}
}