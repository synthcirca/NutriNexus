//import Recipe from '../../models/Recipe';
import type { Recipe } from '../../models/Recipe';
import type { RecipeDto } from '../../models/recipeDto';
import RecipeListItem from './RecipeListItem';

interface RecipeListProps {
  recipes: Recipe[];
}

export default function RecipeList({ recipes }: RecipeListProps) {
  const items = recipes.map((recipe) => <RecipeListItem recipe={recipe} />);
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 max-w-4xl">
      {items}
    </div>
  );
}
