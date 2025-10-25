namespace NutriNexusAPI.Entities
{
    public class MealItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
