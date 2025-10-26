using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace NutriNexusAPI.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeStep> RecipeSteps { get; set; } = new();
        //public List<Ingredient> Ingredients { get; set; } = new(); 
        public int TimeEstimate { get; set; }
        public int ServingSize { get; set; }
    }
}
