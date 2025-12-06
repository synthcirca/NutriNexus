// import React from 'react';
// import apiConnector from '../../api/apiConnector';
// import type { RecipeDto } from '../../models/recipeDto';

// interface Props {
//   recipe: RecipeDto;
// }

// export default function RecipeTableItem({ recipe }: Props) {
//   return (
//     <>
//       <tr className="center aligned">
//         <td data-label="Id">{recipe.id}</td>
//         <td data-label="Name">{recipe.name}</td>
//         <td data-label="Time Estimate">{recipe.timeEstimate}</td>
//         <td data-label="Seriving Size">{recipe.servingSize}</td>
//         <td data-label="Action">
//           <button>Edit</button>
//           <button
//             onClick={async () => {
//               await apiConnector.deleteMovie(recipe.id!);
//               window.location.reload();
//             }}
//           >
//             Delete
//           </button>
//         </td>
//       </tr>
//     </>
//   );
// }
