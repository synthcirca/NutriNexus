// __tests__/RecipeTable.test.tsx
import React from 'react';
import { render, screen, waitFor, fireEvent } from '@testing-library/react';
import RecipeTable from './RecipeTable';
import apiConnector from '../../api/apiConnector';
import type { RecipeDto } from '../../models/recipeDto';

// Mock the API module
// jest.mock('../src/api/apiConnector', () => ({
//   getRecipes: jest.fn(),
// }));

// Helper to create fake recipes
// const fakeRecipes: RecipeDto[] = [
//   { id: 1, name: 'Pancakes', timeEstimate: 15, servingSize: 4, recipeSteps: []},
//   { id: 2, name: 'Salad', timeEstimate: 10, servingSize: 2, recipeSteps: []},
// ];

describe('RecipeTable', () => {
  it('renders the table fields', () => {
    //expect()
  });
});
