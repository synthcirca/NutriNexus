using NutriNexusAPI.DTO;

namespace NutriNexus.Api.DTO
{
    // For PUT (full update)
    public class RecipeUpdateRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string TotalTime { get; set; }
        public int ServingSize { get; set; }
        public string Course { get; set; }
        public string Cuisine { get; set; }
        public string Author { get; set; }
        public string SourceUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Notes { get; set; }
        public List<RecipeIngredientCreateRequest> RecipeIngredients { get; set; }
        public List<RecipeEquipmentCreateRequest> RecipeEquipment { get; set; }
        public List<RecipeInstructionCreateRequest> RecipeInstructions { get; set; }
    }
}
