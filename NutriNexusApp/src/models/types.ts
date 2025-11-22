export interface Recipe {
  id: string;          // UUID or numeric id
  title: string;
  description: string;
  ingredients: string[];
  steps: string[];
}