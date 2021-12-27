import api from "../api/myApi";

export const login = (data) => async (dispatch) => {
    const response = await api.post('/api/Account/authenticate', data);
    if(response.status===200){
        if(response.data.succeeded){
            let userInfo = {
                jwt: response.data.data.jwToken,
                email: response.data.data.email,
                userName: response.data.data.userName,
                roles : response.data.data.roles,
                isAuth: true
            }
            localStorage.setItem('userInfo', JSON.stringify(userInfo));
            localStorage.setItem('token', response.data.data.jwToken);
            
            dispatch({
                type: "login",
                payload: userInfo
            });
        }
        else{
            dispatch({
                type: "error",
                payload: response.data.Message
            });
        }
    }
}
export const getUserInfo = () => (dispatch) => {    
    let item = localStorage.getItem('userInfo');
    let userInfo = null;
    if(item !== null){
        userInfo = JSON.parse(item);
    }
    dispatch({
        type: "login",
        payload: userInfo
    });
}
export const logout = () => async (dispatch) => {
    localStorage.clear();
    dispatch({
        type: "logout",
        payload: null
    });
}