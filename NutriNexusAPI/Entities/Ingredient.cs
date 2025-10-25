namespace NutriNexusAPI.Entities
{
    public class Ingredient
    {
        int Id { get; set; }
        string Name { get; set; }
        float Calories { get; set; }
        Unit CaloriesUnit { get; set; }
    }
}
