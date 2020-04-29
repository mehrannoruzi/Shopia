import actionTypes from './actionTypes';

export function SetAddrssAction(address, reciever, recieverMobileNumber) {
    return {
        type: actionTypes.SET_ADDRESS,
        payload: { address, reciever, recieverMobileNumber }
    };
};