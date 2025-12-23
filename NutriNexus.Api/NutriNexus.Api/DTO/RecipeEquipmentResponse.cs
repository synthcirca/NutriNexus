namespace NutriNexus.Api.DTO
{
    public class RecipeEquipmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceUrl { get; set; }
        public int? Quantity { get; set; } // Optional: e.g., "2 mixing bowls"
        public string? Notes { get; set; } // Optional: e.g., "must be oven-safe"
    }
}