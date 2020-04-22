import { combineReducers } from 'redux';
import modalReducer from './reducers/modalReducer';
import toastReducer from './reducers/toastReducer';
import initErrorReducer from './reducers/initErrorReducer';
import orderProductReducer from './reducers/orderProductReducer';
import basketReducer from './reducers/basketReducer';

const reducers = combineReducers({
    modalReducer,
    toastReducer,
    initErrorReducer,
    orderProductReducer,
    basketReducer
});

export default reducers;