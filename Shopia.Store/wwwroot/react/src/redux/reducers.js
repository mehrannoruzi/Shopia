import { combineReducers } from 'redux';
import homeReducer from './reducers/homeReducer';
import authenticationReducer from './reducers/authenticationReducer';
import modalReducer from './reducers/modalReducer';
import toastReducer from './reducers/toastReducer';

const reducers = combineReducers({
    authenticationReducer,
    homeReducer,
    modalReducer,
    toastReducer
});

export default reducers;