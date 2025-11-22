import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import React from 'react';
import apiConnector from './api/apiConnector';
import type { RecipeDto } from './models/recipeDto';
import RecipeTableItem from './components/recipes/RecipeTableItem';
import RecipeForm from './components/recipes/RecipeForm';
import type { Recipe } from './models/types';

interface AppState {
  recipes: RecipeDto[];
}

function App() {
  const [recipes, setRecipes] = useState<AppState['recipes']>([]);
  const [selectedRecipe, setSelectedRecipe] = useState<Recipe | null>(null);
  const [isModalOpen, setModalOpen] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      const fetchedRecipes = await apiConnector.getRecipes();
      setRecipes(fetchedRecipes);
    };

    fetchData();
  }, []);

  //so should I create a new recipe object and pass it into here so that it behaves like when you would edit?
  const addNewRecipe = () => {
    //setSelectedRecipe(recipe);
    setModalOpen(true);
    console.log('Modal is Open!');
  };

  const closeModal = () => {
    //setSelectedRecipe(null);
    setModalOpen(false);
  };

  return (
    // <div>
    //   <h1>My Recipes</h1>
    //   <button onClick={() => addNewRecipe()}>Add</button>
    //   {/* {recipes.map((recipe) => (
    //     <RecipeTableItem key={recipe.id} recipe={recipe} />
    //   ))} */}
    //   {isModalOpen && <RecipeForm />}
    // </div>
    <div className="relative">
      <button
        className="bg-green-600 hover:bg-green-500 transition duration-150
      text-white px-5 py-2 rounded-md absolute top-4 left-8 transform"
        onClick={addNewRecipe}
      >
        Show Modal
      </button>
      {isModalOpen && <RecipeForm onClose={closeModal} />}
    </div>
  );
}

export default App;
