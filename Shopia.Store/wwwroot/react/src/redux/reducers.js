import { combineReducers } from 'redux';
import homeReducer from './reducers/homeReducer';
import authenticationReducer from './reducers/authenticationReducer';
import modalReducer from './reducers/modalReducer';
import toastReducer from './reducers/toastReducer';
import drawerReducer from './reducers/drawerReducer';
import orderProductReducer from './reducers/orderProductReducer';

const reducers = combineReducers({
    authenticationReducer,
    homeReducer,
    modalReducer,
    toastReducer,
    drawerReducer,
    orderProductReducer
});

export default reducers;