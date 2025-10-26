import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import React from 'react';
import apiConnector from './api/apiConnector';
import type { RecipeDto } from './models/recipeDto';
import RecipeTableItem from './components/recipes/RecipeTableItem';

interface AppState {
  recipes: RecipeDto[];
}

function App() {
  const [recipes, setRecipes] = useState<AppState['recipes']>([]);

  useEffect(() => {
    const fetchData = async () => {
      const fetchedRecipes = await apiConnector.getRecipes();
      setRecipes(fetchedRecipes);
    };

    fetchData();
  }, []);

  return (
    <div>
      {recipes.map((recipe) => (
        <RecipeTableItem key={recipe.id} recipe={recipe} />
      ))}
    </div>
  );
}

export default App;
