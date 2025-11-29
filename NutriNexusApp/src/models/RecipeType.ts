//import type { RecipeStepDto } from "./recipeStepDto";

export interface RecipeType {
  id: number | undefined;
  name: string;
  rating: number | undefined;
  imageUrl: string;
  prepTime: string;
  cookTime: string;
  totalTime: string | undefined;
  servingSize: number | undefined;
  description: string;
  ingredients: string[] | undefined;
  directions: string[] | undefined;
  nutrition: string[];
  notes: string[];
  categories: string[];
  tags: string[];
}
