import actionTypes from './actionTypes';

export  function ShowInitErrorAction(fetchData) {
    return {
        type: actionTypes.SHOW_INIT_ERROR,
        payload: {
            fetchData
        }
    };
};

export  function HideInitErrorAction() {
    return {
        type: actionTypes.Hide_INIT_ERROR
    };
};


