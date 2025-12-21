namespace NutriNexus.Api.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public float Calories { get; set; }
        public int UnitId { get; set; }
        public int RecipeId { get; set; }
        public float Amount { get; set; }
    }
}
