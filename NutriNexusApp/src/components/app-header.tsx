import { Link } from 'react-router';
export function AppHeader() {
  return (
    <header className="border-b-3 border-border bg-background">
      <div className="container mx-auto px-4 py-4">
        <div className="flex items-center justify-between">
          <h1 className="font-bold text-3xl">NutriNexus</h1>
          <nav className="flex gap-3">
            <Link to="/">Home</Link>
            <Link to="/plan">Plan</Link>
            <Link to="/shop">Shop</Link>
            <Link to="/cook">Cook</Link>
          </nav>
        </div>
      </div>
    </header>
  );
}
