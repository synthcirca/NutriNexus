import { Link } from 'react-router';

export default function NotFoundPage() {
  return (
    <>
      <div className="flex flex-col gap2">
        <h1> 404 Not Found</h1>
        <Link to="/">From Link</Link>
      </div>
    </>
  );
}
