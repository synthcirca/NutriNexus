namespace NutriNexus.Api.Entities
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int IngredientId { get; set; }

        public required string Category { get; set; } //refridgerated, pantry, spices, etc
    }
}
