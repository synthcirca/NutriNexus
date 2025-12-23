using NutriNexusAPI.Entities;

namespace NutriNexus.Api.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public float? Quantity { get; set; }
        public string Unit { get; set; }

        public string? Note { get; set; }
        
        
    }
}
