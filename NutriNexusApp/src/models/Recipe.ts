export type RecipeDetail = {
  id: number;
  name: string;
  imageUrl: string;
  description: string;
  rating: number;
  prepTime: string | undefined;
  cookTime: string | undefined;
  totalTime: string;
  servingSize: number | undefined;
  course: string;
  cusine: string;

  ingredients: RecipeIngredient[] | undefined;
  equpiment: RecipeEquipment[] | undefined;
  instructions: RecipeInstruction[] | undefined;
};

export type RecipeSummary = {
  id: number;
  name: string;
  imageUrl: string;
  description: string;
  rating: number;
  totalTime: string | undefined;
};

export type RecipeIngredient = {
  id: number | undefined;
  name: string;
  quantity: number;
  unit: string;
  calories: number;
};

export type RecipeEquipment = {
  id: number | undefined;
  name: string;
  sourceUrl: string;
  quantity: number;
  notes: string;
};

export type RecipeInstruction = {
  id: number | undefined;
  stepNumber: number;
  instruction: string;
};
