import { useState, useEffect} from "react"
import { type GameDto } from "../../models/gameDto"
import apiConnector from "../../api/apiConnector";
import GameTableItem from "./GameTableItem";

export default function GameTable() {
    const [games, setGames] = useState<GameDto[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const fetchedGames = await apiConnector.getGames();
            setGames(fetchedGames);
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
                    {games.length !== 0 && (
                        games.map((games, index) => (
                            <GameTableItem key={index} game={games} />
                        ))
                    )}
                </tbody>
            </table>
            <button/>
        </>
    )
}