import { MOCK_RECIPES } from '../../data/MockRecipes';
import RecipeList from './RecipeList';

export default function RecipesPage() {
  return (
    <>
      <h1 className="text-4xl text-black font-bold mb-8 border-4 border-black bg-white px-6 py-4 inline-block shadow-[8px_8px_0px_0px_rgba(0,0,0,1)]">
        {' '}
        Recipes
      </h1>
      <RecipeList recipes={MOCK_RECIPES}></RecipeList>
    </>
  );
}
