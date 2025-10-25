import React from "react";
import apiConnector from "../../api/apiConnector";
import type { MovieDto } from "../../models/movieDto"

interface Props {
    movie: MovieDto; 
}

export default function MovieTableItem({movie}: Props) {
    return (
        <>
            <tr className="center aligned">
                <td data-label="Id">{movie.id}</td>
                <td data-label="Title">{movie.title}</td>
                <td data-label="Description">{movie.description}</td>
                <td data-label="CreateDate">{movie.createDate}</td>
                <td data-label="Category">{movie.category}</td>
                <td data-label="Action">
                    <button>Edit</button>
                    <button onClick={async () => {
                        await apiConnector.deleteMovie(movie.id!)
                        window.location.reload(); 
                    } }>Delete</button>
                </td>
            </tr>
        </>
    )
}