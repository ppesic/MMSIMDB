
export const responseHandler = (response, action) => {
    if(response.status === 200){
        if(response.data.succeeded) {
            return {
                type: action,
                payload: response.data.data
            }
        }
        else{
            return {
                type: "error",
                payload: response.data.Message
            }
        }
    }
    else{
        return {
            type: "error",
            payload: response.statusText
        }
    }
}