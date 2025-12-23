namespace NutriNexusAPI.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Calories { get; set; } //per 100g        
    }
}
