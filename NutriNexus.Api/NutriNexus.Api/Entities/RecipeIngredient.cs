using NutriNexusAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace NutriNexus.Api.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public Guid RecipeIngredientId { get; set; }
        public Guid RecipeId { get; set; }
        public required Recipe Recipe { get; set; }
        public Guid IngredientId { get; set; }
        public required Ingredient Ingredient { get; set; }
        public float? Quantity { get; set; }
        public required string Unit { get; set; }
        public string? Note { get; set; }
    }
}
