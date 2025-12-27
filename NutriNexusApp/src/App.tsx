import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import React from 'react';
import apiConnector from './api/apiConnector';
//import type { RecipeDto } from './models/recipeDto';
//import RecipeTableItem from './components/recipes/RecipeTableItem';
//import RecipeModal from './components/recipes/RecipeModal';
//import type { Recipe } from './models/types';
//import RecipeForm from './components/recipes/RecipeForm';
//import RecipeCard from './components/recipes/RecipeCard';
import type { RecipeDetail, RecipeSummary } from './models/Recipe';
//import RecipesPage from './components/recipes/RecipesPage';
import RecipeList from './components/recipes/RecipeList';
import RecipeModal from './components/recipes/RecipeModal';
//import RecipeList from './components/recipes/RecipeList';

interface AppState {
  recipes: RecipeSummary[];
}

function App() {
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
    <div>
      <h1> Recipe List</h1>
      <div className="relative">
        <button onClick={addNewRecipe}>Add New Recipe</button>
        {isModalOpen && (
          <RecipeModal
            recipeDetail={recipe}
            isOpen={isModalOpen}
            onClose={closeModal}
            accentColor="yellow"
          />
        )}
      </div>
      <RecipeList recipes={recipes}></RecipeList>
      {/* <RecipeModal
        recipeDetail={recipe}
        isOpen={isModalOpen}
        onClose={() => setModalOpen(false)}
        accentColor="yellow"
      ></RecipeModal> */}
    </div>
  );
}

export default App;
