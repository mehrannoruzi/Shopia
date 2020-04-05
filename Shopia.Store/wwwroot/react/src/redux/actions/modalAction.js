import actionTypes from './actionTypes';

export function ShowModalAction(title, body) {
    return {
        type: actionTypes.SHOWMODAL,
        show: true,
        title,
        body
    };
};

export function HideModalAction() {
    return {
        type: actionTypes.HIDEMODAL,
        show: false
    };
};