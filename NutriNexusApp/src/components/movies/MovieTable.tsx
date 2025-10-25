import { useState, useEffect} from "react"
import { type MovieDto } from "../../models/movieDto"
import apiConnector from "../../api/apiConnector";
import MovieTableItem from "./MovieTableItem";

export default function MovieTable() {
    const [movies, setMovies] = useState<MovieDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const fetchedMovies = await apiConnector.getMovies();
            setMovies(fetchedMovies);
        }

        fetchData(); 
    }, []);

    return (
        <>
            <table>
                <thead style={{ textAlign: 'center' }}>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>CreateDate</th>
                        <th>Category</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {movies.length !== 0 && (
                        movies.map((movie, index) => (
                            <MovieTableItem key={index} movie={movie} />
                        ))
                    )}
                </tbody>
            </table>
            <button/>
        </>
    )
}