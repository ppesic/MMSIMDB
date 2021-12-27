const authUserReducer = (state = {isAuth: false}, action) => {
    switch(action.type){
        case "login":
            if(action.payload !== null){
                return { ...action.payload };
            }
            return { ...state };        
        default:
            return state;
    }
}
export default authUserReducer;