namespace NutriNexusAPI.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MealItem> MealItems { get; set; }
    }
}
