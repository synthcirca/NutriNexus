"use client"

import type React from "react"

import type { PlannedMeal } from "@/lib/types"
import { MealSlot } from "./meal-slot"
import { ChevronLeft, ChevronRight } from "lucide-react"

interface CalendarGridProps {
  view: "weekly" | "monthly"
  startDate: Date
  plannedMeals: PlannedMeal[]
  onDrop: (date: Date) => void
  onDragOver: (e: React.DragEvent) => void
  onWeekChange: (direction: "prev" | "next") => void
  onRemoveServing: (mealId: string, servingId: string) => void
  onDragStartServing: (mealId: string, servingId: string) => void
}

export function CalendarGrid({
  view,
  startDate,
  plannedMeals,
  onDrop,
  onDragOver,
  onWeekChange,
  onRemoveServing,
  onDragStartServing,
}: CalendarGridProps) {
  const daysToShow = view === "monthly" ? 35 : 7

  const days = Array.from({ length: daysToShow }, (_, i) => {
    const date = new Date(startDate)
    if (view === "monthly") {
      const firstOfMonth = new Date(date.getFullYear(), date.getMonth(), 1)
      const firstDayOfWeek = firstOfMonth.getDay()
      const offset = firstDayOfWeek === 0 ? -6 : 1 - firstDayOfWeek
      firstOfMonth.setDate(firstOfMonth.getDate() + offset + i)
      return firstOfMonth
    } else {
      date.setDate(date.getDate() + i)
      return date
    }
  })

  const getServingsForDate = (date: Date) => {
    const dateStr = date.toDateString()
    const servingsOnDate: Array<{
      meal: PlannedMeal
      serving: { id: string; dayIndex: number }
    }> = []

    for (const meal of plannedMeals) {
      for (const instance of meal.servingInstances) {
        if (instance.date.toDateString() === dateStr) {
          servingsOnDate.push({
            meal,
            serving: { id: instance.id, dayIndex: instance.dayIndex },
          })
        }
      }
    }

    return servingsOnDate
  }

  return (
    <div className="bg-background border-4 border-border shadow-2xs p-6 h-full flex flex-col">
      <div className="flex items-center justify-between mb-4 border-b-4 border-border pb-4">
        <button
          onClick={() => onWeekChange("prev")}
          className="bg-sidebar border-4 border-border shadow-2xs p-2 hover:shadow-sm transition-shadow"
        >
          <ChevronLeft className="w-6 h-6" />
        </button>
        <h2 className="text-2xl font-bold">
          {startDate.toLocaleDateString("en-US", { month: "long", year: "numeric" })}
        </h2>
        <button
          onClick={() => onWeekChange("next")}
          className="bg-sidebar border-4 border-border shadow-2xs p-2 hover:shadow-sm transition-shadow"
        >
          <ChevronRight className="w-6 h-6" />
        </button>
      </div>

      <div className={`grid gap-4 flex-1 ${view === "monthly" ? "grid-cols-7" : "grid-cols-7"}`}>
        {view === "monthly" && (
          <>
            {["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"].map((day) => (
              <div key={day} className="text-center font-bold border-b-2 border-border pb-1">
                {day}
              </div>
            ))}
          </>
        )}

        {days.map((date) => {
          const servingsOnDate = getServingsForDate(date)
          const isCurrentMonth = view === "monthly" && date.getMonth() === startDate.getMonth()

          return (
            <div key={date.toISOString()} className="flex flex-col min-h-0">
              {view === "weekly" && (
                <div className="text-center mb-2 font-bold border-b-2 border-border pb-1">
                  <div className="text-sm">{date.toLocaleDateString("en-US", { weekday: "short" })}</div>
                  <div className="text-lg">{date.getDate()}</div>
                </div>
              )}
              <MealSlot
                date={date}
                servingsOnDate={servingsOnDate}
                onDrop={onDrop}
                onDragOver={onDragOver}
                onRemoveServing={onRemoveServing}
                onDragStartServing={onDragStartServing}
                view={view}
                isCurrentMonth={isCurrentMonth}
              />
            </div>
          )
        })}
      </div>
    </div>
  )
}
