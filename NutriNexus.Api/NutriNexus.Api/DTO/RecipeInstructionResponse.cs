namespace NutriNexus.Api.DTO
{
    public record RecipeInstructionResponse
    {
        public int StepNumber { get; init; }
        public string Instruction { get; init; }
    }
}
