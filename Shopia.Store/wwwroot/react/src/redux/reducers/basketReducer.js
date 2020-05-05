﻿import actionTypes from '../actions/actionTypes';
import basketSrv from './../../service/basketSrv';

const initState = {
    items: basketSrv.get()
};
const caculate = (items) => {
    let totalPrice = items.reduce(function (total, x) {
        return total + (x.realPrice * x.count);
    }, 0);
    let totalDiscount = items.reduce(function (total, x) {
        return total + ((x.price - x.realPrice) * x.count);
    }, 0);
    return { totalPrice, totalDiscount };
}
export default function basketReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.ADD_TO_BASKET:
            let idx = state.items.findIndex(x => x.id == action.payload.id);
            let items = [];
            if (idx === -1)
                items = [...state.items, { ...action.payload }];
            else {
                state.items[idx].count = action.payload.count;
                items = [...state.items];
            }
            return { ...state, items, ...caculate(items) };
        case actionTypes.UPDATE_BASKET:
            let item = state.items.find(x => x.id == action.payload.id);
            if (item) item.count = action.payload.count;
            return { ...state, items: [...state.items], ...caculate(state.items) };
        case actionTypes.REMOVE_FROM_BASKET:
            state.items.splice(state.items.findIndex(x => x.id == action.payload.id), 1);
            return { ...state, items: [...state.items], ...caculate(state.items) };
        case actionTypes.CHANGED_BASKET_ITEMS:
            action.payload.products.forEach(p => {
                let idx = state.items.findIndex(x => x.id === p.id);
                if (idx > -1) {
                    if (p.maxCount === 0) state.items.splice(idx, 1);
                    else {
                        state.items[idx].price = p.price;
                        state.items[idx].discount = p.discount;
                    }
                }
    
            });
            return { ...state, items: [...state.items], ...caculate(state.items) };
        default:
            return { ...state, ...caculate(state.items) };
    }
};