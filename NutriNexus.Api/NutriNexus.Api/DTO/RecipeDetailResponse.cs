namespace NutriNexus.Api.DTO
{
    public record RecipeDetailResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public decimal Rating { get; init; }
        public TimeSpan PrepTime { get; init; }
        public TimeSpan CookTime { get; init; }
        public TimeSpan TotalTime { get; init; }
        public int ServingSize { get; init; }
        public string Course { get; init; }
        public string Cuisine { get; init; }
        public List<RecipeIngredientResponse> Ingredients { get; init; }
        public List<RecipeEquipmentResponse> Equipment { get; init; }
        public List<RecipeInstructionResponse> RecipeInstructions { get; init; }
    }
}
