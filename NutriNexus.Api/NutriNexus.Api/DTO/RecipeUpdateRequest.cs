namespace NutriNexus.Api.DTO
{
    // For PUT (full update)
    public class RecipeUpdateRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int TotalTime { get; set; }
        public int ServingSize { get; set; }
        public string Course { get; set; }
        public string Cuisine { get; set; }
        public string Author { get; set; }
        public string SourceUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Notes { get; set; }
        public List<RecipeIngredientRequest> Ingredients { get; set; }
        public List<RecipeEquipmentRequest> Equipment { get; set; }
        public List<string> Directions { get; set; }
    }

    // Reuse from create
    public class RecipeIngredientRequest
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public string? Note { get; set; }
    }

    public class RecipeEquipmentRequest
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
