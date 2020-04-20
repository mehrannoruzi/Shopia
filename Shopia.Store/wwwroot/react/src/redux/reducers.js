import { combineReducers } from 'redux';
import authenticationReducer from './reducers/authenticationReducer';
import modalReducer from './reducers/modalReducer';
import toastReducer from './reducers/toastReducer';
import initErrorReducer from './reducers/initErrorReducer';
import orderProductReducer from './reducers/orderProductReducer';
import basketIconReducer from './reducers/basketIconReducer';

const reducers = combineReducers({
    authenticationReducer,
    modalReducer,
    toastReducer,
    initErrorReducer,
    orderProductReducer,
    basketIconReducer
});

export default reducers;