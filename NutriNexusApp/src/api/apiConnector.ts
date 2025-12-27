import axios, { type AxiosResponse } from 'axios';
import { API_BASE_URL } from '../router/routes';
import type {
  RecipeSummary,
  RecipeDetail,
  RecipeCreateRequest,
} from '@/models/Recipe';

const apiConnector = {
  getRecipes: async (): Promise<RecipeSummary[]> => {
    try {
      const response: AxiosResponse = await axios.get(`${API_BASE_URL}/meals`);
      console.log(response.data);
      console.log(response.data.recipeDtos);
      const recipe = response.data.map((recipe: RecipeSummary) => ({
        ...recipe,
      }));
      console.log(recipe);
      return recipe;
    } catch (error) {
      console.log('Error fetching recipes:', error);
      throw error;
    }
  },
  getRecipeById: async (recipeId: number): Promise<RecipeDetail> => {
    try {
      const response = await axios.get<RecipeDetail>(
        `${API_BASE_URL}/meals/${recipeId}`
      );
      console.log(response.data);
      return response.data;
    } catch (error) {
      console.log(error);
      throw error;
    }
  },
  updateRecipe: async (
    recipeId: number,
    recipeData: RecipeDetail
  ): Promise<RecipeDetail> => {
    try {
      console.log('Recipe Data: ', recipeData);
      const response = await axios.put<RecipeDetail>(
        `${API_BASE_URL}/meals/${recipeId}`,
        recipeData
      );
      console.log('Recipe updated:', response.data);
      return response.data;
    } catch (error) {
      console.log('Error updating recipe:', error);
      throw error;
    }
  },
  createRecipe: async (
    recipeData: RecipeCreateRequest
  ): Promise<RecipeCreateRequest> => {
    try {
      console.log('Sending recipeData: ', recipeData);
      const response = await axios.post<RecipeCreateRequest>(
        `${API_BASE_URL}/meals`,
        recipeData
      );
      console.log('Recipe created:', response.data);
      return response.data;
    } catch (error) {
      console.log('Error creating recipe:', error);
      throw error;
    }
  },
};

export default apiConnector;
