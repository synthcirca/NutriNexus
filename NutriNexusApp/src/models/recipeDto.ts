import type { RecipeStepDto } from "./recipeStepDto";

export interface RecipeDto {
    id: number | undefined,
    name: string,
    timeEstimate: number | undefined,
    servingSize: number | undefined,
    recipeSteps: RecipeStepDto
}

