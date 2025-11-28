import type React from 'react';
import { useState } from 'react';
import { Pencil } from 'lucide-react';
import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';

interface RecipeData {
  image: string;
  title: string;
  description: string;
  ingredients: string[];
  directions: string[];
}

export default function RecipeCard() {
  const [recipe, setRecipe] = useState<RecipeData>({
    image: '/delicious-pasta-dish.jpg',
    title: 'Classic Spaghetti Carbonara',
    description:
      'A traditional Italian pasta dish with eggs, cheese, pancetta, and black pepper.',
    ingredients: [
      '400g spaghetti',
      '200g pancetta or guanciale',
      '4 large eggs',
      '100g Pecorino Romano cheese',
      'Black pepper to taste',
      'Salt for pasta water',
    ],
    directions: [
      'Bring a large pot of salted water to boil and cook spaghetti according to package directions.',
      'While pasta cooks, dice the pancetta and cook in a large skillet until crispy.',
      'In a bowl, whisk together eggs and grated Pecorino Romano cheese.',
      'Reserve 1 cup pasta water, then drain the spaghetti.',
      'Remove skillet from heat, add hot pasta to pancetta and toss.',
      'Add egg mixture and toss quickly, adding pasta water as needed to create a creamy sauce.',
      'Season with black pepper and serve immediately with extra cheese.',
    ],
  });

  const [editingField, setEditingField] = useState<string | null>(null);
  const [tempValue, setTempValue] = useState<string>('');

  const handleEdit = (field: string, value: string) => {
    setEditingField(field);
    setTempValue(value);
  };

  const handleSave = (field: keyof RecipeData) => {
    if (field === 'ingredients' || field === 'directions') {
      setRecipe((prev) => ({
        ...prev,
        [field]: tempValue.split('\n').filter((item) => item.trim() !== ''),
      }));
    } else {
      setRecipe((prev) => ({
        ...prev,
        [field]: tempValue,
      }));
    }
    setEditingField(null);
  };

  const handleKeyDown = (e: React.KeyboardEvent, field: keyof RecipeData) => {
    if (
      e.key === 'Enter' &&
      !e.shiftKey &&
      field !== 'ingredients' &&
      field !== 'directions'
    ) {
      e.preventDefault();
      handleSave(field);
    } else if (e.key === 'Escape') {
      setEditingField(null);
    }
  };

  return (
    <Card className="overflow-hidden border-2 border-border bg-card shadow-lg">
      {/* Image Section */}
      <div
        className="group relative h-64 cursor-pointer overflow-hidden bg-muted md:h-80"
        onClick={() => handleEdit('image', recipe.image)}
      >
        <img
          src={recipe.image || '/placeholder.svg'}
          alt={recipe.title}
          className="h-full w-full object-cover transition-transform duration-300 group-hover:scale-105"
        />
        <div className="absolute inset-0 flex items-center justify-center bg-black/0 transition-all duration-300 group-hover:bg-black/50">
          <div className="translate-y-4 opacity-0 transition-all duration-300 group-hover:translate-y-0 group-hover:opacity-100">
            <Pencil className="h-8 w-8 text-white" />
          </div>
        </div>
      </div>

      <div className="p-6 md:p-8">
        {/* Title Section */}
        <div className="mb-4">
          {editingField === 'title' ? (
            <input
              type="text"
              value={tempValue}
              onChange={(e) => setTempValue(e.target.value)}
              onBlur={() => handleSave('title')}
              onKeyDown={(e) => handleKeyDown(e, 'title')}
              className="w-full border-b-2 border-accent bg-transparent text-3xl font-bold text-foreground outline-none md:text-4xl"
              autoFocus
            />
          ) : (
            <h2
              onClick={() => handleEdit('title', recipe.title)}
              className="group cursor-pointer text-balance text-3xl font-bold text-foreground transition-colors hover:text-accent md:text-4xl"
            >
              {recipe.title}
              <Pencil className="ml-2 inline-block h-5 w-5 opacity-0 transition-opacity group-hover:opacity-100" />
            </h2>
          )}
        </div>

        {/* Description Section */}
        <div className="mb-6">
          {editingField === 'description' ? (
            <textarea
              value={tempValue}
              onChange={(e) => setTempValue(e.target.value)}
              onBlur={() => handleSave('description')}
              onKeyDown={(e) => handleKeyDown(e, 'description')}
              className="w-full border-2 border-accent bg-transparent p-2 text-pretty leading-relaxed text-muted-foreground outline-none"
              rows={2}
              autoFocus
            />
          ) : (
            <p
              onClick={() => handleEdit('description', recipe.description)}
              className="group cursor-pointer text-pretty leading-relaxed text-muted-foreground transition-colors hover:text-foreground"
            >
              {recipe.description}
              <Pencil className="ml-2 inline-block h-4 w-4 opacity-0 transition-opacity group-hover:opacity-100" />
            </p>
          )}
        </div>

        <div className="grid gap-6 md:grid-cols-2">
          {/* Ingredients Section */}
          <div>
            <h3 className="mb-3 text-xl font-bold uppercase tracking-wide text-accent">
              Ingredients
            </h3>
            {editingField === 'ingredients' ? (
              <textarea
                value={tempValue}
                onChange={(e) => setTempValue(e.target.value)}
                onBlur={() => handleSave('ingredients')}
                className="w-full rounded-md border-2 border-accent bg-transparent p-3 text-foreground outline-none"
                rows={8}
                placeholder="Enter each ingredient on a new line"
                autoFocus
              />
            ) : (
              <ul
                onClick={() =>
                  handleEdit('ingredients', recipe.ingredients.join('\n'))
                }
                className="group cursor-pointer space-y-2"
              >
                {recipe.ingredients.map((ingredient, index) => (
                  <li
                    key={index}
                    className="flex items-start gap-3 text-foreground transition-colors hover:text-accent"
                  >
                    <span className="mt-1.5 h-1.5 w-1.5 flex-shrink-0 rounded-full bg-accent" />
                    <span className="leading-relaxed">{ingredient}</span>
                  </li>
                ))}
                <li className="flex items-center gap-2 pt-2 opacity-0 transition-opacity group-hover:opacity-100">
                  <Pencil className="h-4 w-4 text-accent" />
                  <span className="text-sm text-muted-foreground">
                    Click to edit
                  </span>
                </li>
              </ul>
            )}
          </div>

          {/* Directions Section */}
          <div>
            <h3 className="mb-3 text-xl font-bold uppercase tracking-wide text-accent">
              Directions
            </h3>
            {editingField === 'directions' ? (
              <textarea
                value={tempValue}
                onChange={(e) => setTempValue(e.target.value)}
                onBlur={() => handleSave('directions')}
                className="w-full rounded-md border-2 border-accent bg-transparent p-3 text-foreground outline-none"
                rows={8}
                placeholder="Enter each step on a new line"
                autoFocus
              />
            ) : (
              <ol
                onClick={() =>
                  handleEdit('directions', recipe.directions.join('\n'))
                }
                className="group cursor-pointer space-y-3"
              >
                {recipe.directions.map((direction, index) => (
                  <li
                    key={index}
                    className="flex gap-3 text-foreground transition-colors hover:text-accent"
                  >
                    <span className="flex h-6 w-6 flex-shrink-0 items-center justify-center rounded-full bg-accent text-sm font-bold text-accent-foreground">
                      {index + 1}
                    </span>
                    <span className="leading-relaxed">{direction}</span>
                  </li>
                ))}
                <li className="flex items-center gap-2 pt-2 opacity-0 transition-opacity group-hover:opacity-100">
                  <Pencil className="h-4 w-4 text-accent" />
                  <span className="text-sm text-muted-foreground">
                    Click to edit
                  </span>
                </li>
              </ol>
            )}
          </div>
        </div>
      </div>
    </Card>
  );
}
