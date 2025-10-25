import React from "react";
import apiConnector from "../../api/apiConnector";
import type { GameDto } from "../../models/gameDto"

interface Props {
    game: GameDto; 
}

export default function GameTableItem({game}: Props) {
    return (
        <>
            <tr className="center aligned">
                <td data-label="Id">{game.id}</td>
                <td data-label="Name">{game.name}</td>
                <td data-label="Genre">{game.genre}</td>
                <td data-label="ReleaseDate">{game.releaseDate}</td>
                <td data-label="Price">{game.price}</td>
                <td data-label="Action">
                    <button>Edit</button>
                    <button onClick={async () => {
                        await apiConnector.deleteMovie(game.id!)
                        window.location.reload(); 
                    } }>Delete</button>
                </td>
            </tr>
        </>
    )
}

/*export interface GameDto {
    id: number | undefined,
    name: string,
    genreId: number | undefined,
    genre: number | undefined,
    price: number | undefined,
    releaseDate: string | undefined
}
*/