import actionTypes from '../actions/actionTypes';

const initState = {
    address: null,
    cost: 0,
    reciever: '',
    mobileNumber: ''
};

export default function basketReducer(state = initState, action) {
    switch (action.type) {
        case actionTypes.SELECT_ADDRESS:
            return { ...action.payload }
        default:
            return { ...state };
    }
};