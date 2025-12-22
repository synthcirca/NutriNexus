namespace NutriNexusAPI.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Calories { get; set; }
        public int UnitId { get; set; }
        public float Amount {get; set;}
    }
}
