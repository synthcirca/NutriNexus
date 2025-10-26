import { useState, useEffect } from 'react';
import apiConnector from '../../api/apiConnector';
import RecipeTableItem from './RecipeTableItem';
import type { RecipeDto } from '../../models/recipeDto';

export default function RecipeTable() {
  const [recipes, setRecipes] = useState<RecipeDto[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const fetchedRecipes = await apiConnector.getRecipes();
      setRecipes(fetchedRecipes);
    };

    fetchData();
  }, []);

  return (
    <>
      <table>
        <thead style={{ textAlign: 'center' }}>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Time Estimate</th>
            <th>Seriving Size</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {recipes.length !== 0 &&
            recipes.map((recipes, index) => (
              <RecipeTableItem key={index} recipe={recipes} />
            ))}
        </tbody>
      </table>
      <button />
    </>
  );
}
