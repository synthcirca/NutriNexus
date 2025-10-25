import { useEffect, useState } from 'react';
import './App.css';
import GameTable from './components/games/GameTable';
import axios from 'axios'; 
import React from 'react';
import type { GameDto } from './models/gameDto';
import GameTableItem from './components/games/GameTableItem';
import apiConnector from './api/apiConnector';
import type { RecipeDto } from './models/recipeDto';
import RecipeTableItem from './components/recipes/RecipeTableItem';
/*function App() {
    return (
        <>
            <GameTable></GameTable>
        </>
    )
}*/

//interface GenreProps {
//    genre: Genre;
//}

//const Genre = ({ genre }: GenreProps) => {
//    return (
//        <div>
//            <h2>{genre.Name}</h2>
//        </div>
//    );
//};

//interface GameProps {
//    game: Game;
//}
//const Game = ({ game }: GameProps) => {
//    return (
//        <div>
//            <h2>{game.Name}</h2>
//            <p>Release Date: {game.ReleaseDate}</p>
//            <p>Price: {game.Price}</p>
//            <Genre genre={game.Genre} />
//        </div>
//    );
//};

interface AppState {
    recipes: RecipeDto[];
}

function App() {
    const [recipes, setRecipes] = useState<AppState['recipes']>([]);

    //useEffect(() => {
    //    axios.get('http://localhost:5279/games/')
    //        .then(response => {
    //            console.log(response);
    //            setGames(response.data);
    //        });
    //}, []);

    useEffect(() => {
        const fetchData = async () => {
            const fetchedRecipes = await apiConnector.getRecipes();
            setRecipes(fetchedRecipes);
        }

        fetchData();
    }, []);

    return (
        <div>
            {recipes.map((recipe) => (
                <RecipeTableItem key={recipe.id} recipe={recipe} />
            ))}
        </div>
    );
}


export default App;