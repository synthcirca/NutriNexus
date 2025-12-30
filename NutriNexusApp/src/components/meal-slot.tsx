"use client"

import type React from "react"

import type { PlannedMeal } from "@/lib/types"
import { GripVertical, X } from "lucide-react"

interface MealSlotProps {
  date: Date
  servingsOnDate: Array<{
    meal: PlannedMeal
    serving: { id: string; dayIndex: number }
  }>
  onDrop: (date: Date) => void
  onDragOver: (e: React.DragEvent) => void
  onRemoveServing: (mealId: string, servingId: string) => void
  onDragStartServing: (mealId: string, servingId: string) => void
  view?: "weekly" | "monthly"
  isCurrentMonth?: boolean
}

export function MealSlot({
  date,
  servingsOnDate,
  onDrop,
  onDragOver,
  onRemoveServing,
  onDragStartServing,
  view = "weekly",
  isCurrentMonth = true,
}: MealSlotProps) {
  const isToday = new Date().toDateString() === date.toDateString()

  const handleDragStart = (e: React.DragEvent, mealId: string, servingId: string) => {
    e.stopPropagation()
    onDragStartServing(mealId, servingId)
  }

  return (
    <div
      onDrop={() => onDrop(date)}
      onDragOver={onDragOver}
      className={`border-4 border-border p-2 transition-colors relative ${
        view === "monthly" ? "min-h-24" : "min-h-32"
      } ${isToday ? "bg-secondary/20" : "bg-card"} ${
        !isCurrentMonth && view === "monthly" ? "opacity-40" : ""
      } hover:bg-muted/50 flex flex-col gap-2`}
    >
      {view === "monthly" && <div className="absolute top-1 left-1 text-xs font-bold z-10">{date.getDate()}</div>}

      {servingsOnDate.map(({ meal, serving }) => (
        <div
          key={`${meal.id}-${serving.id}`}
          draggable
          onDragStart={(e) => handleDragStart(e, meal.id, serving.id)}
          className={`bg-accent text-accent-foreground border-4 border-border shadow-2xs p-2 relative group cursor-move ${
            view === "monthly" ? "mt-4" : ""
          } flex-shrink-0`}
        >
          <div className="absolute -top-2 -left-2 bg-accent-foreground text-accent border-4 border-border w-6 h-6 flex items-center justify-center">
            <GripVertical className="w-3 h-3" />
          </div>
          <button
            onClick={() => onRemoveServing(meal.id, serving.id)}
            className="absolute -top-2 -right-2 bg-destructive text-destructive-foreground border-4 border-border w-6 h-6 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity z-10"
          >
            <X className="w-3 h-3" />
          </button>
          <h4 className={`font-bold mb-1 ${view === "monthly" ? "text-xs" : "text-sm"}`}>{meal.recipe.name}</h4>
          <p className="text-xs font-mono">
            Day {serving.dayIndex + 1}/{meal.totalServings} • {meal.peopleEating} person
          </p>
          {view === "weekly" && <p className="text-xs font-mono">Total servings: {meal.recipe.servings}</p>}
        </div>
      ))}
    </div>
  )
}
