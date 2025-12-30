export default function HomePage() {
  return (
    <div className="container mx-auto px-4 py-12">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-6xl font-bold mb-6 text-foreground">Welcome to MealPlanner</h1>

        <div className="bg-primary text-primary-foreground border-4 border-border shadow-lg p-8 mb-8">
          <p className="text-2xl font-bold mb-4">Plan your meals, shop smarter, cook better!</p>
          <p className="text-xl">
            Your all-in-one solution for meal planning, grocery shopping, and cooking delicious meals.
          </p>
        </div>

        <div className="grid md:grid-cols-3 gap-6">
          <div className="bg-secondary text-secondary-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">📅 Plan</h2>
            <p className="font-bold">Organize your weekly meals with ease</p>
          </div>

          <div className="bg-accent text-accent-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">🛒 Shop</h2>
            <p className="font-bold">Generate smart shopping lists</p>
          </div>

          <div className="bg-primary text-primary-foreground border-4 border-border shadow-md p-6">
            <h2 className="text-2xl font-bold mb-2">👨‍🍳 Cook</h2>
            <p className="font-bold">Access recipes and cooking guides</p>
          </div>
        </div>
      </div>
    </div>
  )
}
