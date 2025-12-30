import { useEffect, useState } from 'react';
import './App.css';
import './globals.css';
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
import { BrutalistBox } from './components/ui/BrutalistBox';
import { AppHeader } from './components/app-header';
import { Outlet } from 'react-router';

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
    // <div>
    //   {/* <AppHeader></AppHeader>
    //   <div className="relative">
    //     <button
    //       className="h-12 border-black border-2 p-2.5 bg-[#A6FAFF] hover:bg-[#79F7FF] hover:shadow-[2px_2px_0px_rgba(0,0,0,1)] active:bg-[#00E1EF] rounded-full"
    //       onClick={addNewRecipe}
    //     >
    //       Add New Recipe
    //     </button>
    //     {isModalOpen && (
    //       <RecipeModal
    //         recipeDetail={recipe}
    //         isOpen={isModalOpen}
    //         onClose={closeModal}
    //         accentColor="yellow"
    //       />
    //     )}
    //   </div>
    //   <RecipeList recipes={recipes}></RecipeList> */}
    //   <MealPlannerPage></MealPlannerPage>
    //   {/* <div className="w-96 px-8 py-4 bg-white border-4 border-black shadow-[3px_3px_0px_rgba(0,0,0,1)] grid place-content-center">
    //     <div>
    //       <h1 className="text-2xl mb-4">The message you want goes in here.</h1>
    //       <div className="flex space-x-2 mx-auto w-32">
    //         <button className="text-base">Cancel</button>
    //         <button className="h-12 border-black border-2 p-2.5 bg-[#A6FAFF] hover:bg-[#79F7FF] hover:shadow-[2px_2px_0px_rgba(0,0,0,1)] active:bg-[#00E1EF] rounded-full">
    //           Enable
    //         </button>
    //       </div>
    //     </div>
    //   </div> */}
    // </div>

    <>
      <AppHeader />
      <main>
        <Outlet />
      </main>
      {/* <Footer /> */}
    </>
  );
}

export default App;
