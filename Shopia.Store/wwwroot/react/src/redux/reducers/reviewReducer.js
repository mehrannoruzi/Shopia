﻿import actionTypes from '../actions/actionTypes';

const initState = {
    address: null,
    cost: 0,
    reciever: '',
    mobileNumber: ''
};

export default function basketReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.SET_ADDRESS:
            return { ...state, ...action.payload }
        default:
            return { ...state };
    }
};