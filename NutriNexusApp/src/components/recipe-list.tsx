'use client';

import type { Recipe } from '../models/Recipe';
import { SAMPLE_RECIPES } from '../data/sample-recipes';
import { RecipeCard } from './recipe-card';

interface RecipeListProps {
  onDragStart: (recipe: Recipe, adjustedServings: number) => void;
}

export function RecipeList({ onDragStart }: RecipeListProps) {
  return (
    <div className="bg-sidebar border-4 border-border shadow-2xs p-6 h-full overflow-y-auto">
      <h2 className="text-2xl font-bold mb-4 border-b-4 border-border pb-2">
        RECIPES
      </h2>
      <div className="space-y-4">
        {SAMPLE_RECIPES.map((recipe) => (
          <RecipeCard
            key={recipe.id}
            recipe={recipe}
            onDragStart={onDragStart}
          />
        ))}
      </div>
    </div>
  );
}
