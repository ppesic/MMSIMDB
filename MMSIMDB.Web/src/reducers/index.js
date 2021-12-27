import { combineReducers } from "redux";
//import { reducer as formReducer } from 'redux-form';
import authUserReducer from './authUserReducer';
import moviesReducer from './moviesReducer';
import statusReducer from './statusReducer';

export default combineReducers({
    authUser: authUserReducer,
    movies: moviesReducer,
    status: statusReducer
});