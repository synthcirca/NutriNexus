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


export type RecipeSummary = {
  id: number;
  name: string;
  imageUrl: string;
  description: string;
  rating: number;
  totalTime: string | undefined;
};


export type RecipeCreateRequest = {
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
  author: string;
  sourceUrl: string;
  videoUrl: string;
  notes: string;
  ingredients: RecipeIngredientCreateRequest[];
  instructions: RecipeInstructionsCreateRequest[];
  equipment: RecipeEquipmentCreateRequest[];
};

export type RecipeIngredientCreateRequest = {
  name: string;
  quantity: number;
  unit: string;
  notes: string;
};

export type RecipeEquipmentCreateRequest = {
  name: string;
  description: string;
  sourceUrl: string;
  quantity: number;
  notes: string;
};

export type RecipeInstructionsCreateRequest = {
  stepNumber: number;
  instruction: string;
};


export function mapRecipeDetailToCreateRequest(
  recipeDetail: RecipeDetail
): RecipeCreateRequest {
  return {
    name: recipeDetail.name,
    imageUrl: recipeDetail.imageUrl,
    description: recipeDetail.description,
    rating: recipeDetail.rating,
    prepTime: recipeDetail.prepTime,
    cookTime: recipeDetail.cookTime,
    totalTime: recipeDetail.totalTime,
    servingSize: recipeDetail.servingSize,
    course: recipeDetail.course,
    cusine: recipeDetail.cusine,
    author: '', // RecipeDetail doesn't have author, provide default
    sourceUrl: '', // RecipeDetail doesn't have sourceUrl, provide default
    videoUrl: '', // RecipeDetail doesn't have videoUrl, provide default
    notes: '', // RecipeDetail doesn't have notes, provide default
    ingredients: (recipeDetail.ingredients || []).map((ingredient) => ({
      name: ingredient.name,
      quantity: ingredient.quantity,
      unit: ingredient.unit,
      notes: '', // RecipeIngredient doesn't have notes, provide default
    })),
    instructions: (recipeDetail.instructions || []).map((instruction) => ({
      stepNumber: instruction.stepNumber,
      instruction: instruction.instruction,
    })),
    equipment: (recipeDetail.equpiment || []).map((equipment) => ({
      name: equipment.name,
      description: '', // RecipeEquipment doesn't have description, provide default
      sourceUrl: equipment.sourceUrl,
      quantity: equipment.quantity,
      notes: equipment.notes,
    })),
  };
}