//import Recipe from '../../models/Recipe';
import type { RecipeSummary } from '../../models/Recipe';
import RecipeListItem from './RecipeListItem';

interface RecipeListProps {
  recipes: RecipeSummary[];
}

export default function RecipeList({ recipes }: RecipeListProps) {
  const items = recipes.map((recipe) => <RecipeListItem recipe={recipe} />);
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 max-w-4xl">
      {items}
    </div>
  );
}
