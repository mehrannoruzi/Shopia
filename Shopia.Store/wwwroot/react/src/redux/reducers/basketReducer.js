import actionTypes from '../actions/actionTypes';
import basketSrv from './../../service/basketSrv';

const initState = {
    items: basketSrv.get()
};

export default function basketReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.ADD_TO_BASKET:
            let idx = state.items.findIndex(x => x.id == action.payload.id);
            console.log(idx);
            if (idx === -1)
                return { ...state, items: [...state.items, { ...action.payload }] };
            else {
                state.items[idx].count = action.payload.count;
                return { ...state, items: [...state.items] };
            }
        case actionTypes.REMOVE_FROM_BASKET:
            state.items.splice(state.items.findIndex(x => x.id == action.payload.id), 1);
            return { ...state, items: [...state.items] };
        default:
            return { ...state };
    }
};