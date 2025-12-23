namespace NutriNexusAPI.Entities
{
    public class RecipeEquipment
    {
        public int Id { get; set; }
		public int RecipeId {get; set;}
        public Recipe Recipe { get; set;}
        public int EquipmentId { get; set;}
        public Equipment Equipment { get; set;}
        public int? Quantity { get; set; } // Optional: e.g., "2 mixing bowls"
        public string? Notes { get; set; } // Optional: e.g., "must be oven-safe"
    }
}
