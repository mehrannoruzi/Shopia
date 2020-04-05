import actionTypes from './actionTypes';
import CryptoJS from 'crypto-js';

export function LogInAction(token, userId, username) {
    console.log(token);
    let user = {
        token: token,
        userId: userId,
        username: username
    };
    let userInfo = JSON.stringify(user);
    let encInfo = CryptoJS.AES.encrypt(userInfo, 'kingofday.ir').toString();
    localStorage.setItem('user', encInfo);
    return {
        type: actionTypes.LOGIN,
        token: token,
        userId: userId,
        username: username
    };
};

export function LogOutAction() {
    localStorage.removeItem('user');
    return {
        type: actionTypes.LOGOUT,
        token: null
    };
};