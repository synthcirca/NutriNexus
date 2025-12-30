'use client';

import { useState } from 'react';
import type { Recipe } from '../models/Recipe';
import { Minus, Plus } from 'lucide-react';

interface RecipeCardProps {
  recipe: Recipe;
  onDragStart: (recipe: Recipe, adjustedServings: number) => void;
}

export function RecipeCard({ recipe, onDragStart }: RecipeCardProps) {
  const [adjustedServings, setAdjustedServings] = useState(recipe.servings);

  const handleIncrement = () => {
    setAdjustedServings((prev) => prev + 1);
  };

  const handleDecrement = () => {
    setAdjustedServings((prev) => Math.max(1, prev - 1));
  };

  return (
    <div
      draggable
      onDragStart={() => onDragStart(recipe, adjustedServings)}
      className="bg-primary text-primary-foreground border-4 border-border shadow-2xs p-4 cursor-grab active:cursor-grabbing hover:shadow-sm transition-shadow"
    >
      <h3 className="font-bold text-lg mb-2">{recipe.name}</h3>
      <div className="space-y-2 text-sm">
        <div className="flex items-center gap-2">
          <span className="font-mono">Servings:</span>
          <button
            onClick={(e) => {
              e.stopPropagation();
              handleDecrement();
            }}
            className="w-6 h-6 border-2 border-border bg-secondary hover:bg-accent flex items-center justify-center"
          >
            <Minus className="w-3 h-3" />
          </button>
          <span className="font-bold text-base w-8 text-center">
            {adjustedServings}
          </span>
          <button
            onClick={(e) => {
              e.stopPropagation();
              handleIncrement();
            }}
            className="w-6 h-6 border-2 border-border bg-secondary hover:bg-accent flex items-center justify-center"
          >
            <Plus className="w-3 h-3" />
          </button>
        </div>
        <p>
          <span className="font-mono">Category:</span> {recipe.category}
        </p>
        <p>
          <span className="font-mono">Prep Time:</span> {recipe.prepTime}
        </p>
      </div>
    </div>
  );
}
