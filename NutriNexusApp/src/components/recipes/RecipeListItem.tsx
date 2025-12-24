import type { RecipeSummary, RecipeDetail } from '../../models/Recipe';
//import Image from '../../assets/delicious-pasta-dish.jpg';
//import CardStyles from './RecipeListItem.module.css';
import RecipeModal from './RecipeModal';
import apiConnector from '../../api/apiConnector';
import { useState } from 'react';

function formatDescription(description: string): string {
  return description.substring(0, 80) + '...';
}

interface RecipeListItemProps {
  recipe: RecipeSummary;
}

export default function RecipeListItem(props: RecipeListItemProps) {
  const { recipe } = props;
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [recipeDetail, setRecipeDetail] = useState<RecipeDetail | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchRecipeDetail = async (recipeId: number) => {
    setIsLoading(true);
    setError(null);

    try {
      const data = await apiConnector.getRecipeById(recipeId);
      setRecipeDetail(data);
      setIsModalOpen(true);
    } catch (err) {
      setError(
        err instanceof Error ? err.message : 'Failed to load recipe details'
      );
      console.error('Error fetching recipe detail:', err);
    } finally {
      setIsLoading(false);
    }
  };

  const handleClick = () => {
    fetchRecipeDetail(recipe.id);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setRecipeDetail(null);
  };

  return (
    <>
      <div
        onClick={handleClick}
        className="w-64 h-73 aspect-square bg-white border-4 border-black shadow-[8px_8px_0px_0px_rgba(0,0,0,1)] hover:translate-x-1 hover:translate-y-1 hover:shadow-[4px_4px_0px_0px_rgba(0,0,0,1)] transition-all cursor-pointer flex flex-col"
      >
        <div className="w-full h-40 border-b-4 border-black overflow-hidden bg-blue-200 flex-shrink-0">
          <img
            src={recipe.imageUrl}
            alt="Recipe"
            className="w-full h-full object-cover"
          />
        </div>

        <div className="p-3 flex flex-col justify-between flex-1 min-h-0">
          <div className="flex-1">
            <h3 className="font-bold text-lg mb-1 leading-tight text-black">
              {recipe.name}
            </h3>
            <p className="text-xs text-gray-700">
              {formatDescription(recipe.description)}
            </p>
          </div>

          <div className="flex items-center justify-between pt-2 border-t-2 border-black text-black">
            <span className="text-xs font-bold bg-yellow-300 px-2 py-1 border-2 border-black">
              {recipe.totalTime}
            </span>
            <span className="text-xs font-bold">⭐ 4.8</span>
          </div>
        </div>
      </div>

      {recipeDetail && (
        <RecipeModal
          recipeDetail={recipeDetail}
          isOpen={isModalOpen}
          onClose={handleCloseModal}
          accentColor="yellow"
        />
      )}
      {error && (
        <div className="text-red-600 text-xs mt-1 font-bold">{error}</div>
      )}
    </>
  );
}
