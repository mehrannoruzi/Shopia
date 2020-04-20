import actionTypes from '../actions/actionTypes';
import basketSrv from './../../service/basket';

const initState = {
    count: basketSrv.getCount()
};

export default function basketIconReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.ADD_TO_BASKET:
            return { ...state, count: state.count + 1 };
        case actionTypes.REMOVE_FROM_BASKET:
            let newCount = state.count - 1;
            return { ...state, count: newCount < 0 ? 0 : newCount };
        default:
            return { ...state };
    }
};