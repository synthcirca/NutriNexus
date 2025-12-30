import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
//import './index.css'
import App from './App.tsx';

import { createBrowserRouter, RouterProvider } from 'react-router';
import NotFoundPage from './pages/NoteFoundPage';
import MealPlannerPage from './page';
import HomePage from './pages/home/page.tsx';
import CookPage from './pages/cook/page.tsx';
import ShopPage from './pages/shop/page.tsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <App></App>,
    errorElement: <NotFoundPage></NotFoundPage>,
    children: [
      {
        path: '/',
        element: <HomePage />,
      },
      {
        path: '/plan',
        element: <MealPlannerPage />,
      },
      {
        path: '/shop',
        element: <ShopPage />,
      },
      {
        path: '/cook',
        element: <CookPage />,
      },
    ],
  },
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router}></RouterProvider>
  </StrictMode>
);
