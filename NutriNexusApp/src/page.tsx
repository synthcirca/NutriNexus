'use client';

import type React from 'react';

import { useState } from 'react';
import type { Recipe, PlannedMeal, ServingInstance } from './models/Recipe';
import { RecipeList } from './components/recipe-list';
import { CalendarGrid } from '@/components/calendar-grid';
import { Calendar, CalendarDays } from 'lucide-react';

export default function MealPlannerPage() {
  const [draggedRecipe, setDraggedRecipe] = useState<{
    recipe: Recipe;
    adjustedServings: number;
  } | null>(null);
  const [draggedServing, setDraggedServing] = useState<{
    mealId: string;
    servingId: string;
  } | null>(null);
  const [plannedMeals, setPlannedMeals] = useState<PlannedMeal[]>([]);
  const [peopleEating, setPeopleEating] = useState(1);
  const [view, setView] = useState<'weekly' | 'monthly'>('weekly');
  const [startDate, setStartDate] = useState(() => {
    const today = new Date();
    const day = today.getDay();
    const diff = today.getDate() - day + (day === 0 ? -6 : 1);
    return new Date(today.setDate(diff));
  });

  const handleDragStart = (recipe: Recipe, adjustedServings: number) => {
    setDraggedRecipe({ recipe, adjustedServings });
    setDraggedServing(null);
  };

  const handleDragStartServing = (mealId: string, servingId: string) => {
    setDraggedServing({ mealId, servingId });
    setDraggedRecipe(null);
  };

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
  };

  const handleDrop = (date: Date) => {
    if (draggedServing) {
      const updatedMeals = plannedMeals.map((meal) => {
        if (meal.id === draggedServing.mealId) {
          const updatedInstances = meal.servingInstances.map((instance) =>
            instance.id === draggedServing.servingId
              ? { ...instance, date: new Date(date) }
              : instance
          );
          return { ...meal, servingInstances: updatedInstances };
        }
        return meal;
      });
      setPlannedMeals(updatedMeals);
      setDraggedServing(null);
      return;
    }

    if (!draggedRecipe) return;

    const totalServings = Math.ceil(
      draggedRecipe.adjustedServings / peopleEating
    );
    const servingInstances: ServingInstance[] = [];

    for (let i = 0; i < totalServings; i++) {
      const servingDate = new Date(date);
      servingDate.setDate(servingDate.getDate() + i);
      servingInstances.push({
        id: `serving-${Date.now()}-${i}`,
        date: servingDate,
        dayIndex: i,
      });
    }

    const newMeal: PlannedMeal = {
      id: `meal-${Date.now()}`,
      recipe: draggedRecipe.recipe,
      peopleEating,
      totalServings,
      servingInstances,
    };

    setPlannedMeals([...plannedMeals, newMeal]);
    setDraggedRecipe(null);
  };

  const handleWeekChange = (direction: 'prev' | 'next') => {
    const newDate = new Date(startDate);
    const daysToShift = view === 'monthly' ? 30 : 7;
    newDate.setDate(
      newDate.getDate() + (direction === 'next' ? daysToShift : -daysToShift)
    );
    setStartDate(newDate);
  };

  const handleRemoveServing = (mealId: string, servingId: string) => {
    const updatedMeals = plannedMeals
      .map((meal) => {
        if (meal.id === mealId) {
          const updatedInstances = meal.servingInstances.filter(
            (instance) => instance.id !== servingId
          );
          return { ...meal, servingInstances: updatedInstances };
        }
        return meal;
      })
      .filter((meal) => meal.servingInstances.length > 0); // Remove meal if no servings left

    setPlannedMeals(updatedMeals);
  };

  return (
    <div className="h-screen flex flex-col p-6 gap-6">
      <h1>Hello </h1>
      <header className="bg-primary text-primary-foreground border-4 border-border shadow-2xs p-6">
        <h1 className="text-4xl font-bold mb-2">MEAL PLANNER</h1>
        <p className="text-lg">
          Drag recipes to calendar and watch them span across multiple days
        </p>
      </header>

      <div className="bg-sidebar border-4 border-border shadow-2xs p-4 flex items-center gap-4">
        <label className="font-bold text-lg">PEOPLE EATING:</label>
        <div className="flex gap-2">
          {[1, 2, 3, 4].map((num) => (
            <button
              key={num}
              onClick={() => setPeopleEating(num)}
              className={`w-12 h-12 border-4 border-border font-bold text-lg transition-all ${
                peopleEating === num
                  ? 'bg-accent text-accent-foreground shadow-2xs'
                  : 'bg-card text-foreground hover:shadow-2xs'
              }`}
            >
              {num}
            </button>
          ))}
        </div>
        <div className="ml-6 flex gap-2">
          <button
            onClick={() => setView('weekly')}
            className={`px-4 py-2 border-4 border-border font-bold flex items-center gap-2 transition-all ${
              view === 'weekly'
                ? 'bg-accent text-accent-foreground shadow-2xs'
                : 'bg-card text-foreground hover:shadow-2xs'
            }`}
          >
            <CalendarDays className="w-5 h-5" />
            WEEK
          </button>
          <button
            onClick={() => setView('monthly')}
            className={`px-4 py-2 border-4 border-border font-bold flex items-center gap-2 transition-all ${
              view === 'monthly'
                ? 'bg-accent text-accent-foreground shadow-2xs'
                : 'bg-card text-foreground hover:shadow-2xs'
            }`}
          >
            <Calendar className="w-5 h-5" />
            MONTH
          </button>
        </div>
        <div className="ml-auto text-sm font-mono bg-muted border-4 border-border p-3">
          <strong>TIP:</strong> Adjust servings before dragging, then
          move/delete individual servings
        </div>
      </div>

      <div className="flex-1 grid grid-cols-[350px_1fr] gap-6 overflow-hidden">
        <RecipeList onDragStart={handleDragStart} />
        <CalendarGrid
          view={view}
          startDate={startDate}
          plannedMeals={plannedMeals}
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onWeekChange={handleWeekChange}
          onRemoveServing={handleRemoveServing}
          onDragStartServing={handleDragStartServing}
        />
      </div>
    </div>
  );
}
