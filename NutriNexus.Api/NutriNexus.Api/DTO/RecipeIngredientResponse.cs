namespace NutriNexus.Api.DTO
{
    public class RecipeIngredientResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public float? Quantity { get; set; }
        public string Unit { get; set; }
        public float? Calories { get; set; }
    }
}