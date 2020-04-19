import actionTypes from './../actions/actionTypes';
export default function modalReducer(state = { open: false, payload: {} }, action) {
    switch (action.type) {
        case actionTypes.TOGGLEDRAWER:
            return { ...state, open: !state.open };
        default:
            return { ...state };
    }
};