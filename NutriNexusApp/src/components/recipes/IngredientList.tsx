'use client';

import { useState, useRef, useEffect, type KeyboardEvent } from 'react';
import Checkbox from './ui/Checkbox';

interface Ingredient {
  id: string;
  value: string;
}

interface IngredientListProps {
  onIngredientsChange?: (ingredients: string[]) => void;
  initialIngredients?: string[];
  className?: string;
}

export function IngredientList({
  onIngredientsChange,
  initialIngredients = [],
  className,
}: IngredientListProps) {
  const [ingredients, setIngredients] = useState<Ingredient[]>(() => {
    if (initialIngredients.length > 0) {
      return [
        ...initialIngredients.map((value, index) => ({
          id: `ingredient-${Date.now()}-${index}`,
          value,
        })),
        { id: `ingredient-${Date.now()}-new`, value: '' },
      ];
    }
    return [{ id: `ingredient-${Date.now()}-0`, value: '' }];
  });

  const inputRefs = useRef<{ [key: string]: HTMLInputElement | null }>({});

  useEffect(() => {
    if (onIngredientsChange) {
      const validIngredients = ingredients
        .map((ing) => ing.value.trim())
        .filter((value) => value !== '');
      onIngredientsChange(validIngredients);
    }
  }, [ingredients, onIngredientsChange]);

  const handleInputChange = (id: string, value: string) => {
    setIngredients((prev) => {
      const updated = prev.map((ing: Ingredient) =>
        ing.id === id ? { ...ing, value } : ing
      );

      const cleaned = updated.filter((ing: Ingredient, idx: number) => {
        // Keep the ingredient being edited
        if (ing.id === id) return true;
        // Keep the last ingredient
        if (idx === updated.length - 1) return true;
        // Keep ingredients with content
        return ing.value.trim() !== '';
      });

      // If typing in the last input and it has content, add a new empty input
      const lastIngredient: Ingredient = cleaned[cleaned.length - 1];
      if (lastIngredient.id === id && value.trim() !== '') {
        return [...cleaned, { id: `ingredient-${Date.now()}`, value: '' }];
      }

      return cleaned;
    });
  };

  const handleKeyDown = (index: number, e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      // Add new ingredient below current one
      const newIngredient = { id: `ingredient-${Date.now()}`, value: '' };
      setIngredients((prev) => {
        const updated = [...prev];
        updated.splice(index + 1, 0, newIngredient);
        return updated;
      });
      // Focus the new input after state update
      setTimeout(() => {
        inputRefs.current[newIngredient.id]?.focus();
      }, 0);
    } else if (e.key === 'Tab' && !e.shiftKey) {
      // If on the last input, create a new one
      if (index === ingredients.length - 1) {
        e.preventDefault();
        const newIngredient = { id: `ingredient-${Date.now()}`, value: '' };
        setIngredients((prev) => [...prev, newIngredient]);
        setTimeout(() => {
          inputRefs.current[newIngredient.id]?.focus();
        }, 0);
      }
    }
  };

  const handleBlur = (id: string, index: number) => {
    setIngredients((prev) => {
      // Don't remove if it has content
      if (prev[index].value.trim() !== '') {
        return prev;
      }
      // Remove empty ingredients that aren't the last one
      return prev.filter((ing) => ing.id !== id);
    });
  };

  return (
    <div className={`space-y-3 ${className}`}>
      <label className="block text-lg font-bold uppercase tracking-tight">
        Ingredients
      </label>
      <div className="space-y-2">
        {ingredients.map((ingredient, index) => (
          <div key={ingredient.id} className="flex items-center gap-1">
            <span className="text-sm font-bold text-muted-foreground select-none">
              <Checkbox />
            </span>
            <input
              ref={(el) => {
                inputRefs.current[ingredient.id] = el;
              }}
              type="text"
              value={ingredient.value}
              onChange={(e) => handleInputChange(ingredient.id, e.target.value)}
              onKeyDown={(e) => handleKeyDown(index, e)}
              onBlur={() => handleBlur(ingredient.id, index)}
              placeholder={
                index === 0 ? 'Click to add ingredient...' : 'Add another...'
              }
              className="flex-1 bg-background px-4 text-base font-medium transition-all placeholder:text-muted-foreground/50 focus:shadow-md focus:translate-x-0.5 focus:translate-y-0.5 focus:outline-none focus:ring-4 focus:ring-ring"
            />
          </div>
        ))}
      </div>
    </div>
  );
}
