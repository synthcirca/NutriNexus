export default function CookPage() {
  return (
    <div className="container mx-auto px-4 py-12">
      <div className="max-w-4xl mx-auto">
        <h1 className="text-5xl font-bold mb-6 text-foreground">Cooking Guides</h1>

        <div className="bg-primary text-primary-foreground border-4 border-border shadow-lg p-8 mb-8">
          <p className="text-xl font-bold">Follow step-by-step cooking instructions and discover new recipes to try.</p>
        </div>

        <div className="bg-card border-4 border-border shadow-md p-8">
          <h2 className="text-2xl font-bold mb-4">Coming Soon</h2>
          <p className="text-lg font-bold text-muted-foreground">Your cooking guides and recipes will appear here.</p>
        </div>
      </div>
    </div>
  )
}
