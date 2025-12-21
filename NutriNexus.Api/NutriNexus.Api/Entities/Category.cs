using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
	public enum CategoryType{
		Cuisine,
		Course
	}
	public class Category
	{
		public long CategoryId { get; set; }
		public string Name { get; set; } = null!;
		public CategoryType Type { get; set; }   // enum: Cuisine | Course
	}

}