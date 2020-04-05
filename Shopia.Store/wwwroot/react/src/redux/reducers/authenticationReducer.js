import actionTypes from './../actions/actionTypes';
import strings from './../../shared/constant';
import CryptoJS from 'crypto-js';

const getInitilState = () => {
    if (!localStorage) {
        alert(strings.browserIsOld);
        return {
            token: null,
            userId: null,
            username: ''
        };
    }

    try {
        let userInfo = localStorage.getItem('user');
        if(userInfo == null)  return {
            token: null,
            userId: null,
            username: ''
        };
        let bytes = CryptoJS.AES.decrypt(userInfo,'kingofday.ir');
        let user = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
        return {
            token: user.token,
            userId: user.userId,
            username: user.username
        };
    }
    catch{
        return {
            token: null,
            userId: null,
            username: ''
        };
    }
}
const authenticationReducer = (state = getInitilState(), action) => {
    switch (action.type) {
        case actionTypes.LOGIN:
            return {
                ...state,
                token: action.token,
                userId: action.userId,
                username: action.username
            };
        case actionTypes.LOGOUT:
            return {
                ...state,
                token: null,
                userId: null,
                username: ''
            };
        default:
            return {...state};
    }
};


export default authenticationReducer;