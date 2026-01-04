using System.ComponentModel.DataAnnotations;

namespace NutriNexusAPI.Entities
{
    public class RecipeEquipment
    {
        [Key]
        public Guid RecipeEquipmentId { get; set; }
		public Guid RecipeId {get; set;}
        public required Recipe Recipe { get; set;}
        public Guid EquipmentId { get; set;}
        public required Equipment Equipment { get; set;}
        public int? Quantity { get; set; } // Optional: e.g., "2 mixing bowls"
        public string? Notes { get; set; } // Optional: e.g., "must be oven-safe"
    }
}
