import { useEffect, useState } from 'react';
import type { RecipeDetail, RecipeSummary } from '@/models/Recipe';
import RecipeList from '@/components/recipes/RecipeList';
import axios from 'axios';
import React from 'react';
import apiConnector from '@/api/apiConnector';

interface AppState {
  recipes: RecipeSummary[];
}

export default function HomePage() {
  const [recipes, setRecipes] = useState<AppState['recipes']>([]);
  const [selectedRecipe, setSelectedRecipe] = useState<RecipeSummary | null>(
    null
  );
  const [isModalOpen, setModalOpen] = useState(true);

  const [recipe, setRecipe] = useState<RecipeDetail>({
    id: -1,
    name: 'Recipe Title',
    imageUrl: '/delicious-pasta-dish.jpg',
    description: 'Add descriptions',
    rating: -1,
    prepTime: '00:00:00',
    cookTime: '00:00:00',
    totalTime: '00:00:00',
    servingSize: -1,
    course: 'choose a course',
    cusine: 'choose a cusine',
    ingredients: [
      {
        id: -1,
        name: 'something',
        quantity: -1,
        unit: 'something',
        calories: -1,
      },
    ],
    equpiment: [
      {
        id: -1,
        name: 'something',
        sourceUrl: 'source',
        quantity: -1,
        notes: 'something',
      },
    ],
    instructions: [
      {
        id: -1,
        stepNumber: 1,
        instruction: 'instruction',
      },
    ],
  });

  useEffect(() => {
    const fetchData = async () => {
      const fetchedRecipes = await apiConnector.getRecipes();
      setRecipes(fetchedRecipes);
    };

    fetchData();
  }, []);

  //so should I create a new recipe object and pass it into here so that it behaves like when you would edit?
  const addNewRecipe = () => {
    setSelectedRecipe(recipe);
    setModalOpen(true);
    console.log('Modal is Open!');
  };

  const closeModal = () => {
    setSelectedRecipe(null);
    setModalOpen(false);
  };

  return (
    <div className="container mx-auto px-4 py-12">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-6xl font-bold mb-6 text-foreground">
          Welcome to MealPlanner
        </h1>

        <div className="bg-primary text-primary-foreground border-4 border-border shadow-lg p-8 mb-8">
          <p className="text-2xl font-bold mb-4">
            Plan your meals, shop smarter, cook better!
          </p>
          <p className="text-xl">
            Your all-in-one solution for meal planning, grocery shopping, and
            cooking delicious meals.
          </p>
        </div>

        <div className="grid md:grid-cols-3 gap-6">
          <div className="bg-secondary text-secondary-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">📅 Plan</h2>
            <p className="font-bold">Organize your weekly meals with ease</p>
          </div>

          <div className="bg-accent text-accent-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">🛒 Shop</h2>
            <p className="font-bold">Generate smart shopping lists</p>
          </div>

          <div className="bg-primary text-primary-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">👨‍🍳 Cook</h2>
            <p className="font-bold">Access recipes and cooking guides</p>
          </div>
        </div>

        <RecipeList recipes={recipes}></RecipeList>
      </div>
    </div>
  );
}
