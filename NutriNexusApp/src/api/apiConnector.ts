//import axios, { type AxiosResponse } from 'axios';
//import { API_BASE_URL } from '../router/routes';
//import type { RecipeDto } from '../models/recipeDto';
//import type { GetGamesResponse } from "../models/getGamesResponse";

const apiConnector = {
  // getRecipes: async (): Promise<RecipeDto[]> => {
  //   try {
  //     const response: AxiosResponse = await axios.get(`${API_BASE_URL}/meals`);
  //     console.log(response.data);
  //     console.log(response.data.recipeDtos);
  //     const recipe = response.data.map((recipe) => ({
  //       ...recipe,
  //     }));
  //     console.log(recipe);
  //     return recipe;
  //   } catch (error) {
  //     console.log('Error fetching recipes:', error);
  //     throw error;
  //   }
  // },
  // deleteRecipe: async (recipeId: number): Promise<void> => {
  //   try {
  //     await axios.delete<number>(`${API_BASE_URL}/meals/${recipeId}`);
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // },
  // getGames: async (): Promise<GameDto[]> => {
  //   try {
  //     const response: AxiosResponse = await axios.get(`${API_BASE_URL}/games`);
  //     console.log(response.data);
  //     console.log(response.data.gameDtos);
  //     const games = response.data.map((game) => ({
  //       ...game,
  //     }));
  //     console.log(games);
  //     return games;
  //   } catch (error) {
  //     console.log('Error fetching games:', error);
  //     throw error;
  //   }
  // },
  // getMovies: async (): Promise<MovieDto[]> => {
  //   try {
  //     const response: AxiosResponse<GetMoviesResponse> = await axios.get(
  //       '${API_BASE_URL}/movies'
  //     );
  //     const movies = response.data.movieDtos.map((movie) => ({
  //       ...movie,
  //       createDate: movie.createDate?.slice(0, 10) ?? '',
  //     }));
  //     return movies;
  //   } catch (error) {
  //     console.log('Error fetching movies:', error);
  //     throw error;
  //   }
  // },
  // createMovie: async (movie: MovieDto): Promise<void> => {
  //   try {
  //     await axios.post<number>('${API_BASE_URL}/movies', movie);
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // },
  // editMovie: async (movie: MovieDto): Promise<void> => {
  //   try {
  //     await axios.put<number>('${API_BASE_URL}/movies', movie);
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // },
  // deleteMovie: async (movieId: number): Promise<void> => {
  //   try {
  //     await axios.delete<number>(`${API_BASE_URL}/movies/${movieId}`);
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // },
  // getMovieById: async (movieId: number): Promise<MovieDto | undefined> => {
  //   try {
  //     const response = await axios.get<GetMoviesByIdResponse>(
  //       `${API_BASE_URL}/movies/${movieId}`
  //     );
  //     return response.data.movieDto;
  //   } catch (error) {
  //     console.log(error);
  //     throw error;
  //   }
  // },
};

export default apiConnector;
