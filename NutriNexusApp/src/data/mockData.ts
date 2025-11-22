import { Recipe } from "./types";

export const recipes: Recipe[] = [
  {
    id: "1",
    title: "Classic Pancakes",
    description: "Fluffy pancakes with maple syrup",
    ingredients: ["Flour", "Eggs", "Milk", "Baking powder", "Salt"],
    steps: [
      "Mix dry ingredients.",
      "Add wet ingredients and whisk.",
      "Cook on a hot griddle until golden."
    ]
  },
  // … add more recipes as needed
];