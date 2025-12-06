// import React, { useState } from 'react';

// //import { Recipe } from '../../models/types';
// //import styles from './RecipeModal.module.css';
// //import { RiCloseLine } from 'react-icons/ri';

// import { Recipe } from '@/models/Recipe';
// import { Pencil } from 'lucide-react';

// import { IngredientList } from './IngredientList';

// interface RecipeType {
//   id: number | undefined;
//   name: string;
//   description: string;
//   imageUrl: string;
//   timeEstimate: string | undefined;
//   rating: number | undefined;
//   servingSize: number | undefined;
//   ingredients: string[] | undefined;
//   directions: string[] | undefined;
// }

// interface RecipeModalProps {
//   recipe: Recipe;
//   isOpen: boolean;
//   onClose: () => void;
//   title: string;
//   description: string;
//   cookTime: string;
//   rating: number;
//   imageUrl: string;
//   accentColor: 'yellow' | 'green' | 'orange' | 'blue';
// }

// export default function RecipeModal({
//   isOpen,
//   onClose,
//   title,
//   description,
//   cookTime,
//   rating,
//   imageUrl,
//   accentColor,
// }: RecipeModalProps) {
//   console.log('[v0] RecipeModal isOpen:', isOpen, 'title:', title);

//   if (!isOpen) return null;

//   const accentColors = {
//     yellow: 'bg-yellow-300',
//     green: 'bg-green-300',
//     orange: 'bg-orange-300',
//     blue: 'bg-blue-300',
//   };

//   const bgColors = {
//     yellow: 'bg-yellow-200',
//     green: 'bg-green-200',
//     orange: 'bg-orange-200',
//     blue: 'bg-blue-200',
//   };

//   const [editingField, setEditingField] = useState<string | null>(null);
//   const [recipe, setRecipe] = useState<Recipe>({
//     id: -1,
//     name: 'Classic Spaghetti Carbonara',
//     description:
//       'A traditional Italian pasta dish with eggs, cheese, pancetta, and black pepper.',
//     imageUrl: '/delicious-pasta-dish.jpg',
//     timeEstimate: 50,
//     servingSize: 4,
//     ingredients: [
//       '400g spaghetti',
//       '200g pancetta or guanciale',
//       '4 large eggs',
//       '100g Pecorino Romano cheese',
//       'Black pepper to taste',
//       'Salt for pasta water',
//     ],
//     directions: [
//       'Bring a large pot of salted water to boil and cook spaghetti according to package directions.',
//       'While pasta cooks, dice the pancetta and cook in a large skillet until crispy.',
//       'In a bowl, whisk together eggs and grated Pecorino Romano cheese.',
//       'Reserve 1 cup pasta water, then drain the spaghetti.',
//       'Remove skillet from heat, add hot pasta to pancetta and toss.',
//       'Add egg mixture and toss quickly, adding pasta water as needed to create a creamy sauce.',
//       'Season with black pepper and serve immediately with extra cheese.',
//     ],
//   });
//   const [tempValue, setTempValue] = useState<string>('');

//   const handleEdit = (field: string, value: string) => {
//     setEditingField(field);
//     setTempValue(value);
//   };

//   const handleSave = (field: keyof RecipeType) => {
//     if (field === 'ingredients' || field === 'directions') {
//       setRecipe((prev) => ({
//         ...prev,
//         [field]: tempValue.split('\n').filter((item) => item.trim() !== ''),
//       }));
//     } else {
//       setRecipe((prev) => ({
//         ...prev,
//         [field]: tempValue,
//       }));
//     }
//     setEditingField(null);
//   };

//   const handleKeyDown = (e: React.KeyboardEvent, field: keyof RecipeType) => {
//     if (
//       e.key === 'Enter' &&
//       !e.shiftKey &&
//       field !== 'ingredients' &&
//       field !== 'directions'
//     ) {
//       e.preventDefault();
//       handleSave(field);
//     } else if (e.key === 'Escape') {
//       setEditingField(null);
//     }
//   };

//   return (
//     //Background - black background that you can click on to close out of the modal
//     <div
//       className="text-black fixed inset-0 bg-amber-200 bg-opacity-50 flex items-center justify-center z-50 p-4"
//       onClick={onClose}
//     >
//       {/* The card itself */}
//       <div
//         className="bg-blue-300 border-4 border-black shadow-[16px_16px_0px_0px_rgba(0,0,0,1)] max-w-2xl w-full max-h-[90vh] overflow-auto"
//         onClick={(e) => e.stopPropagation()}
//       >
//         {/* Header */}
//         <div className="flex justify-between items-start p-4 border-b-4 border-black">
//           {editingField === 'title' ? (
//             <input
//               type="text"
//               value={tempValue}
//               onChange={(e) => setTempValue(e.target.value)}
//               onBlur={() => handleSave('name')}
//               onKeyDown={(e) => handleKeyDown(e, 'name')}
//               className="w-full border-b-2 border-accent bg-transparent text-3xl font-bold text-foreground outline-none md:text-4xl"
//               autoFocus
//             />
//           ) : (
//             <h2
//               onClick={() => handleEdit('title', title)}
//               className="group cursor-pointer text-balance text-3xl font-bold text-foreground transition-colors hover:text-accent md:text-4xl"
//             >
//               {title}
//               <Pencil className="ml-2 inline-block h-5 w-5 opacity-0 transition-opacity group-hover:opacity-100" />
//             </h2>
//           )}
//           <button
//             onClick={onClose}
//             className="text-white text-2xl font-bold w-10 h-10 flex items-center justify-center border-2 border-black bg-red-300 hover:bg-red-400 transition-colors ml-4"
//             aria-label="Close modal"
//           >
//             X
//           </button>
//         </div>

//         {/* Image */}
//         <div
//           className={`w-full h-64 border-b-4 border-black overflow-hidden ${bgColors[accentColor]}`}
//         >
//           <img
//             src={imageUrl || '/placeholder.svg'}
//             alt={title}
//             className="w-full h-full object-cover"
//           />
//         </div>

//         {/* Content */}
//         <div className="p-6">
//           {/* Quick Facts */}
//           <div className="flex items-center gap-4 mb-6 pb-4 border-b-2 border-black">
//             {editingField === 'timeEstimate' ? (
//               <>
//                 <input
//                   type="text"
//                   value={tempValue}
//                   onChange={(e) => setTempValue(e.target.value)}
//                   onBlur={() => handleSave('timeEstimate')}
//                   onKeyDown={(e) => handleKeyDown(e, 'timeEstimate')}
//                   className={`text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
//                   autoFocus
//                 />
//               </>
//             ) : (
//               <>
//                 <span
//                   onClick={() => handleEdit('timeEstimate', cookTime)}
//                   className={`text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
//                 >
//                   {cookTime}
//                 </span>
//                 <Pencil className="ml-2 inline-block h-5 w-5 opacity-0 transition-opacity group-hover:opacity-100" />
//               </>
//             )}

//             {editingField === 'rating' ? (
//               <>
//                 ⭐
//                 <input
//                   type="text"
//                   value={tempValue}
//                   onChange={(e) => setTempValue(e.target.value)}
//                   onBlur={() => handleSave('rating')}
//                   onKeyDown={(e) => handleKeyDown(e, 'rating')}
//                   className={`text-lg font-bold`}
//                   autoFocus
//                 />
//               </>
//             ) : (
//               <span
//                 onClick={() => handleEdit('rating', rating.toString())}
//                 className="text-lg font-bold"
//               >
//                 ⭐ {rating}
//               </span>
//             )}
//           </div>

//           {/*Long Facts*/}
//           <div className="space-y-4">
//             {/*Description*/}
//             <div>
//               <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
//                 Description
//               </h3>
//               {editingField === 'description' ? (
//                 <>
//                   <input
//                     type="text"
//                     value={tempValue}
//                     onChange={(e) => setTempValue(e.target.value)}
//                     onBlur={() => handleSave('description')}
//                     onKeyDown={(e) => handleKeyDown(e, 'description')}
//                     className={` border-black border-2 p-2.5 focus:outline-none focus:shadow-[2px_2px_0px_rgba(0,0,0,1)] focus:bg-[#FFA6F6] active:shadow-[2px_2px_0px_rgba(0,0,0,1)]"
//      placeholder="you@example.com" text-sm font-bold ${accentColors[accentColor]} px-3 py-2 border-2 border-black`}
//                     autoFocus
//                   />
//                 </>
//               ) : (
//                 <>
//                   <p
//                     onClick={() => handleEdit('description', description)}
//                     className="text-base"
//                   >
//                     {description}
//                   </p>
//                 </>
//               )}
//             </div>

//             {/*Ingredients*/}
//             {/* <div>
//               <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
//                 Ingredients
//               </h3>
//               <ul className="space-y-2 text-base">
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">•</span> Sample ingredient 1
//                 </li>
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">•</span> Sample ingredient 2
//                 </li>
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">•</span> Sample ingredient 3
//                 </li>
//               </ul>
//             </div> */}
//             <div>
//               <IngredientList initialIngredients={recipe.ingredients} />
//             </div>
//             {/*Instructions*/}
//             <div>
//               <h3 className="text-xl font-bold mb-2 border-b-2 border-black pb-1">
//                 Instructions
//               </h3>
//               <ol className="space-y-2 text-base">
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">1.</span> Sample instruction
//                   step 1
//                 </li>
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">2.</span> Sample instruction
//                   step 2
//                 </li>
//                 <li className="flex items-start">
//                   <span className="mr-2 font-bold">3.</span> Sample instruction
//                   step 3
//                 </li>
//               </ol>
//             </div>
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// }
