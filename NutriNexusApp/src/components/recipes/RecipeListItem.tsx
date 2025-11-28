import { Recipe } from '../../models/Recipe';
import Image from '../../assets/delicious-pasta-dish.jpg';
import CardStyles from './RecipeListItem.module.css';

function formatDescription(description: string): string {
  return description.substring(0, 80) + '...';
}

interface RecipeListItemProps {
  recipe: Recipe;
}

export default function RecipeListItem(props: RecipeListItemProps) {
  const { recipe } = props;
  return (
    // <div
    //   className={`${CardStyles.recipeCard} aspect-square w-48 rounded-lg shadow-lg hover:shadow-xl transition-shadow duration-200 overflow-hidden`}
    // >
    //   <img src={recipe.imageUrl} className="w-full h-full"></img>
    //   {/* <section className="section dark">
    //     <h5 className="strong">
    //       <strong>{recipe.name}</strong>
    //     </h5>
    //     <p>{formatDescription(recipe.description)}</p>
    //     <p>Time Est: {recipe.timeEstimate}</p>
    //   </section> */}
    //   //Optional gradient overlay (for better text readability)
    // <div className="absolute inset-0 bg-gradient-to-t from-black via-transparent to-transparent opacity-60"></div>
    // //Text block
    // <div className="absolute bottom-2 left-2 right-2 text-white">
    //   <h3 className="font-semibold text-lg truncate">Chocolate Cake</h3>
    //   <p className="text-xs truncate">Rich & moist chocolate layered cake</p>
    // </div>
    // </div>

    <div className="w-64 h-73 aspect-square bg-white border-4 border-black shadow-[8px_8px_0px_0px_rgba(0,0,0,1)] hover:translate-x-1 hover:translate-y-1 hover:shadow-[4px_4px_0px_0px_rgba(0,0,0,1)] transition-all cursor-pointer flex flex-col">
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
            30 MIN
          </span>
          <span className="text-xs font-bold">⭐ 4.8</span>
        </div>
      </div>
    </div>
  );
}
