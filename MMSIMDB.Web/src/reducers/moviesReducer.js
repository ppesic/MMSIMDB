import { 
    GET_FIRST_PAGE_MOVIE,
    GET_PAGE_MOVIE,
    ADD_MOVIERATING
} from '../actions/types';

const moviesReducer = (state = [], action) => {
    switch(action.type){
        case GET_FIRST_PAGE_MOVIE:
            return [...action.payload];
        case GET_PAGE_MOVIE:
            return [...state, ...action.payload];
        case ADD_MOVIERATING:
            let item = state.find(el => el.id === action.payload.id);
            if(item !== undefined){
                item.myRating = action.payload.myRating;
                item.ratingSum = action.payload.ratingSum;
                item.numberOfRatings = action.payload.numberOfRatings;
                item.numberOFStars = action.payload.numberOFStars;
            }
            return [...state];
        default:
            return state;
    }
}

export default moviesReducer;