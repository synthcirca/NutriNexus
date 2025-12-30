export function Footer() {
  return (
    <footer className="border-t-4 border-border bg-muted mt-auto">
      <div className="container mx-auto px-4 py-8">
        <div className="flex flex-col md:flex-row items-center justify-between gap-4">
          <div className="text-muted-foreground font-bold">
            © {new Date().getFullYear()} MealPlanner. All rights reserved.
          </div>

          <div className="flex gap-4">
            <a
              href="#"
              className="px-4 py-2 border-4 border-border bg-accent text-accent-foreground font-bold shadow-sm hover:shadow-md transition-all hover:-translate-y-0.5"
            >
              About
            </a>
            <a
              href="#"
              className="px-4 py-2 border-4 border-border bg-secondary text-secondary-foreground font-bold shadow-sm hover:shadow-md transition-all hover:-translate-y-0.5"
            >
              Contact
            </a>
          </div>
        </div>
      </div>
    </footer>
  )
}
