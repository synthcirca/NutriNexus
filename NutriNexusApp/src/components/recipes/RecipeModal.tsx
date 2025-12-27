import React, { useState, useEffect } from 'react';

//import { Recipe } from '../../models/types';
//import styles from './RecipeModal.module.css';
//import { RiCloseLine } from 'react-icons/ri';

import { mapRecipeDetailToCreateRequest, type RecipeDetail } from '@/models/Recipe';
import { Pencil, Save } from 'lucide-react';
import apiConnector from '../../api/apiConnector';

//import { IngredientList } from './IngredientList';

interface RecipeModalProps {
  recipeDetail: RecipeDetail;
  isOpen: boolean;
  onClose: () => void;
  accentColor: 'yellow' | 'green' | 'orange' | 'blue';
}

export default function RecipeModal({
  recipeDetail,
  isOpen,
  onClose,
  accentColor,
}: RecipeModalProps) {
  console.log('[v0] RecipeModal isOpen:', isOpen);
  console.log('RecipeModal: recipeDetail', recipeDetail);
  if (!isOpen) return null;

  const accentColors = {
    yellow: 'bg-yellow-300',
    green: 'bg-green-300',
    orange: 'bg-orange-300',
    blue: 'bg-blue-300',
  };

  const bgColors = {
    yellow: 'bg-yellow-200',
    green: 'bg-green-200',
    orange: 'bg-orange-200',
    blue: 'bg-blue-200',
  };

  const [editingField, setEditingField] = useState<string | null>(null);
  const [recipe, setRecipe] = useState<RecipeDetail>(recipeDetail);
  const [tempValue, setTempValue] = useState<string>('');
  const [isSaving, setIsSaving] = useState(false);
  const [saveError, setSaveError] = useState<string | null>(null);
  const [hasChanges, setHasChanges] = useState(false);

  // Update recipe state when recipeDetail prop changes
  useEffect(() => {
    setRecipe(recipeDetail);
    setHasChanges(false);
  }, [recipeDetail]);

  const handleEdit = (field: string, value: string) => {
    setEditingField(field);
    setTempValue(value);
  };

  const handleSave = (field: keyof RecipeDetail) => {
    if (field === 'ingredients' || field === 'instructions') {
      setRecipe((prev: RecipeDetail) => ({
        ...prev,
        [field]: tempValue.split('\n').filter((item) => item.trim() !== ''),
      }));
    } else if (field === 'rating') {
      setRecipe((prev: RecipeDetail) => ({
        ...prev,
        [field]: parseFloat(tempValue) || prev.rating,
      }));
    } else {
      setRecipe((prev: RecipeDetail) => ({
        ...prev,
        [field]: tempValue,
      }));
    }
    console.log(recipe);
    setEditingField(null);
    setHasChanges(true);
  };

  const handleKeyDown = (e: React.KeyboardEvent, field: keyof RecipeDetail) => {
    if (
      e.key === 'Enter' &&
      !e.shiftKey &&
      field !== 'ingredients' &&
      field !== 'instructions'
    ) {
      e.preventDefault();
      handleSave(field);
    } else if (e.key === 'Escape') {
      setEditingField(null);
    }
  };

  const handleSaveRecipe = async () => {
    setIsSaving(true);
    setSaveError(null);

    try {
      if (recipe.id == -1) {
        const createRequest = mapRecipeDetailToCreateRequest(recipe);
        await apiConnector.createRecipe(createRequest);
      } else {
        await apiConnector.updateRecipe(recipe.id, recipe);
      }
      setHasChanges(false);
      // Optionally show success message
      console.log('Recipe saved successfully!');
    } catch (error) {
      setSaveError(
        error instanceof Error ? error.message : 'Failed to save recipe'
      );
      console.error('Error saving recipe:', error);
    } finally {
      setIsSaving(false);
    }
  };

  return (
    //Background - black background that you can click on to close out of the modal
    <div
      className="text-black fixed inset-0 bg-gray-900 bg-opacity-50 flex items-center justify-center z-50 p-4"
      onClick={onClose}
    >
      {/* The card itself */}
      <div
        className="bg-blue-300 border-4 border-black shadow-[16px_16px_0px_0px_rgba(0,0,0,1)] max-w-2xl w-full max-h-[90vh] overflow-auto"
        onClick={(e) => e.stopPropagation()}
      >
        {/* Header */}
        <div className="flex justify-between items-start p-4 border-b-4 border-black">
          {editingField === 'name' ? (
            <input
              type="text"
              value={tempValue}
              onChange={(e) => setTempValue(e.target.value)}
              onBlur={() => handleSave('name')}
              onKeyDown={(e) => handleKeyDown(e, 'name')}
              className="w-full border-b-2 border-accent bg-transparent text-3xl font-bold text-foreground outline-none md:text-4xl"
              autoFocus
            />
          ) : (
            <h2
              onClick={() => handleEdit('name', recipe.name)}
              className="group cursor-pointer text-balance text-3xl font-bold text-foreground transition-colors hover:text-accent md:text-4xl"
            >
              {recipe.name}
              <Pencil className="ml-2 inline-block h-5 w-5 opacity-0 transition-opacity group-hover:opacity-100" />
            </h2>
          )}
          <button
            onClick={onClose}
            className="text-white text-2xl font-bold w-10 h-10 flex items-center justify-center border-2 border-black bg-red-300 hover:bg-red-400 transition-colors ml-4"
            aria-label="Close modal"
          >
            X
          </button>
        </div>

        {/* Image */}
        <div
          className={`w-full h-64 border-b-4 border-black overflow-hidden ${bgColors[accentColor]}`}
        >
          <img
            src={recipe.imageUrl || '/placeholder.svg'}
            alt={recipe.name}
            className="w-full h-full object-cover"
          />
        </div>

        {/* Content */}
        <div className="p-6">
          {/* Quick Facts */}
          <div className="flex items-center gap-4 mb-6 pb-4 border-b-2 border-black">
            {editingField === 'totalTime' ? (
              <>
                <input
                  type="text"
                  value={tempValue}
                  onChange={(e) => setTempValue(e.target.value)}
                  onBlur={() => handleSave('totalTime')}
                  onKeyDown={(e) => handleKeyDown(e, 'totalTime')}
                  className={`text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
                  autoFocus
                />
              </>
            ) : (
              <>
                <span
                  onClick={() => handleEdit('totalTime', recipe.totalTime)}
                  className={`text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
                >
                  {recipe.totalTime}
                </span>
                <Pencil className="ml-2 inline-block h-5 w-5 opacity-0 transition-opacity group-hover:opacity-100" />
              </>
            )}

            {editingField === 'rating' ? (
              <>
                ⭐
                <input
                  type="text"
                  value={tempValue}
                  onChange={(e) => setTempValue(e.target.value)}
                  onBlur={() => handleSave('rating')}
                  onKeyDown={(e) => handleKeyDown(e, 'rating')}
                  className={`text-lg font-bold`}
                  autoFocus
                />
              </>
            ) : (
              <span
                onClick={() => handleEdit('rating', recipe.rating.toString())}
                className="text-lg font-bold"
              >
                ⭐ {recipe.rating}
              </span>
            )}
          </div>

          {/*Long Facts*/}
          <div className="space-y-4">
            {/*Description*/}
            <div>
              <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
                Description
              </h3>
              {editingField === 'description' ? (
                <>
                  <input
                    type="text"
                    value={tempValue}
                    onChange={(e) => setTempValue(e.target.value)}
                    onBlur={() => handleSave('description')}
                    onKeyDown={(e) => handleKeyDown(e, 'description')}
                    className={` border-black border-2 p-2.5 focus:outline-none focus:shadow-[2px_2px_0px_rgba(0,0,0,1)] focus:bg-[#FFA6F6] active:shadow-[2px_2px_0px_rgba(0,0,0,1)]"
     placeholder="you@example.com" text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
                    autoFocus
                  />
                </>
              ) : (
                <>
                  <p
                    onClick={() =>
                      handleEdit('description', recipe.description)
                    }
                    className="text-base"
                  >
                    {recipe.description}
                  </p>
                </>
              )}
            </div>

            {/*Ingredients*/}
            {/* <div>
              <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
                Ingredients
              </h3>
              <ul className="space-y-2 text-base">
                <li className="flex items-start">
                  <span className="mr-2 font-bold">•</span> Sample ingredient 1
                </li>
                <li className="flex items-start">
                  <span className="mr-2 font-bold">•</span> Sample ingredient 2
                </li>
                <li className="flex items-start">
                  <span className="mr-2 font-bold">•</span> Sample ingredient 3
                </li>
              </ul>
            </div> */}
            {/* <div>
              <IngredientList initialIngredients={recipe.ingredients} />
            </div> */}
            {/*Instructions*/}
            <div>
              <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
                Instructions
              </h3>
              <ol className="space-y-2 text-base">
                <li className="flex items-start">
                  <span className="mr-2 font-bold">1.</span> Sample instruction
                  step 1
                </li>
                <li className="flex items-start">
                  <span className="mr-2 font-bold">2.</span> Sample instruction
                  step 2
                </li>
                <li className="flex items-start">
                  <span className="mr-2 font-bold">3.</span> Sample instruction
                  step 3
                </li>
              </ol>
            </div>
          </div>

          <div className="mt-6 pt-4 border-t-2 border-black flex items-center justify-between">
            <button
              onClick={handleSaveRecipe}
              disabled={!hasChanges || isSaving}
              className={`flex items-center gap-2 px-6 py-3 border-2 border-black font-bold text-sm transition-all ${
                hasChanges && !isSaving
                  ? `${accentColors[accentColor]} hover:translate-x-1 hover:translate-y-1 shadow-[4px_4px_0px_0px_rgba(0,0,0,1)] hover:shadow-[2px_2px_0px_0px_rgba(0,0,0,1)] cursor-pointer`
                  : 'bg-gray-300 text-gray-500 cursor-not-allowed'
              }`}
            >
              <Save className="h-5 w-5" />
              {isSaving ? 'Saving...' : 'Save Changes'}
            </button>
            {saveError && (
              <span className="text-red-600 text-sm font-bold">
                {saveError}
              </span>
            )}
            {!hasChanges && !saveError && (
              <span className="text-gray-600 text-sm">No changes to save</span>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
