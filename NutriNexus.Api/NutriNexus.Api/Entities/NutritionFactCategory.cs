using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public class NutritionFactCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
	}
}