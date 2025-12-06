using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 
        public decimal Rating { get; set; } = 0m;
        public string? ImageUrl { get; set; }
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
        public int? TotalTime { get; set; }
        public int ServingSize { get; set; }
        public string? Description { get; set; }
        public string? Course {get; set;}
        public string? Cuisine {get; set;}
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Direction> Directions { get; set; } = new List<Direction>();
        public ICollection<NutritionFact> NutritionFacts { get; set; } = new List<NutritionFact>();
        // public ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
        // public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
        // public string? Notes { get; set; }
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
