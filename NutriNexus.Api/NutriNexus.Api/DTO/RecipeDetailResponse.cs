namespace NutriNexus.Api.DTO
{
    public record RecipeDetailResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public decimal Rating { get; init; }
        public string PrepTime { get; init; }
        public string CookTime { get; init; }
        public string TotalTime { get; init; }
        public int ServingSize { get; init; }
        public string Course { get; init; }
        public string Cuisine { get; init; }
        public List<RecipeIngredientResponse> Ingredients { get; init; }
        public List<RecipeEquipmentResponse> Equipment { get; init; }
        public List<RecipeInstructionResponse> RecipeInstructions { get; init; }
    }
}
