﻿import actionTypes from '../actions/actionTypes';

const initState = {
    show: false,
    fetchData: null
};

export default function initErrorReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.SHOW_INIT_ERROR:
            return { ...state, show: true, fetchData: action.payload.fetchData };
        case actionTypes.Hide_INIT_ERROR:
            return { ...state, show: false };
        default:
            return { ...state };
    }
};