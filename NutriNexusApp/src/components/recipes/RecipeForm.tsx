import React, { useState, FormEvent } from 'react';
//import { Recipe } from '../../models/types';
import formStyles from './RecipeForm.module.css';
import { CiClock1 } from 'react-icons/ci';
import { RiKnifeLine } from 'react-icons/ri';
import { LuCookingPot } from 'react-icons/lu';
import { FaRegClock } from 'react-icons/fa';

import styles from './RecipeForm.module.css';
interface Props {
  onClose: () => void;
}

export default function RecipeForm() {
  const [inputs, setInputs] = useState({});
  return (
    <>
      <div className={formStyles.recipeInfo}>
        <div className={formStyles.recipeInfoChild}>
          <div className={formStyles.recipeInfoLeft}>
            <div
              className={`${formStyles.recipeInfoLeftChild} ${formStyles.imgUpload}`}
            >
              <img
                src="img_girl.jpg"
                alt="Picture of a delicious recipe"
                width="500"
                height="600"
              />
            </div>
            <div className={formStyles.recipeInfoLeftChild}>stars</div>
            <div className={formStyles.recipeInfoLeftChild}>
              nutrition facts
            </div>
          </div>
        </div>
        <div className={formStyles.recipeInfoChild}>
          <h1>Recipe Name</h1>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
            ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
            aliquip ex ea commodo consequat. Duis aute irure dolor in
            reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
            pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
            culpa qui officia deserunt mollit anim id est laborum.
          </p>
          <div className={formStyles.prepTime}>
            <RiKnifeLine style={{ marginBottom: "-3px" }}/>
            <p><b>Prep Time</b></p>
            <p>15 min</p>
          </div>
          <div className={formStyles.prepTime}>
            <LuCookingPot />
            <h5>Cook Time</h5>
            <p>25 min</p>
          </div>
          <div className={formStyles.prepTime}>
            <FaRegClock />
            <h5>Total Time</h5>
            <p>15 min</p>
          </div>
        </div>
      </div>
      <div className={formStyles.recipeIngredients}>
		<h1>Ingredients</h1>
        <label>
          <input
            type="checkbox"
            name="onion"
            //checked={inputs.onion}
            //onChange={handleChange}
          />
          Onion:
        </label>
		<label>
          <input
            type="checkbox"
            name="onion"
            //checked={inputs.onion}
            //onChange={handleChange}
          />
          Onion:
        </label>
		<label>
          <input
            type="checkbox"
            name="onion"
            //checked={inputs.onion}
            //onChange={handleChange}
          />
          Onion:
        </label>
      </div>
      <div className="recipe-instructions">
		<h1>Directions</h1>
		<ol>
  			<li>I'm the first item</li>
  			<li>I'm the second item</li>
		</ol>
	  </div>
    </>
  );
}
