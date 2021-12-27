import { 
    GET_FIRST_PAGE_MOVIE,
    GET_PAGE_MOVIE
} from '../actions/types';

const statusReducer = (state = {hasMore:true}, action) => {
    switch(action.type){
        case GET_FIRST_PAGE_MOVIE:
            return {...state, hasMore:action.payload.length === 10};
        case GET_PAGE_MOVIE:
            return {...state, hasMore:action.payload.length === 10};
        default:
            return state;
    }
}

export default statusReducer;