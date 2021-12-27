import api from "../api/myApi";
import { 
    GET_FIRST_PAGE_MOVIE,
    GET_PAGE_MOVIE,
    GET_MOVIE,
    ADD_MOVIERATING
} from './types';
import { responseHandler } from "../api/responseHandler";

export const getMoviePage = (data) => async (dispatch) => {
    const response = await api.post('/api/v1/Movie/GetMoviePage', data);
    dispatch(responseHandler(response, data.pageNumber === 0 ? GET_FIRST_PAGE_MOVIE : GET_PAGE_MOVIE));
}

export const getMovie = (id) => async (dispatch) => {
    const response = await api.get('/api/v1/Movie/Get/' + id);
    dispatch(responseHandler(response, GET_MOVIE));
}

export const addMovieRating = (data) => async (dispatch) => {
    const response = await api.post('/api/v1/Movie/AddRating', data);
    dispatch(responseHandler(response, ADD_MOVIERATING));
}