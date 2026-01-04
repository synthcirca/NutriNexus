using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.Entities
{
    public class Ingredient
    {
        [Key]
        public Guid IngredientId { get; set; }
        public required string Name { get; set; }
        public float? Calories { get; set; } //per 100g        
    }
}
