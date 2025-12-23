namespace NutriNexusAPI.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // Optional: "9x13 inch baking pan"
        public string SourceUrl { get; set; } // Optional
        public List<RecipeEquipment> RecipeEquipments { get; set; }
    }
}
