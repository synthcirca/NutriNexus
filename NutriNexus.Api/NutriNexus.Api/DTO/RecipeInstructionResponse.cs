namespace NutriNexus.Api.DTO
{
    public record RecipeInstructionResponse
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Instruction { get; set; }
    }
}
