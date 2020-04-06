import actionTypes from './actionTypes';
import { storeUserInfo, removeUserInfo } from './../../shared/utils';

export function LogInAction(token, userId, username) {
    console.log(token);
    let user = {
        token: token,
        userId: userId,
        username: username
    };
    storeUserInfo(user);
    return {
        type: actionTypes.LOGIN,
        token: token,
        userId: userId,
        username: username
    };
};

export function LogOutAction() {
    removeUserInfo();
    return {
        type: actionTypes.LOGOUT,
        token: null
    };
};