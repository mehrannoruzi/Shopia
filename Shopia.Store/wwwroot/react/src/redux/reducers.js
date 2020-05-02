import { combineReducers } from 'redux';
import modalReducer from './reducers/modalReducer';
import toastReducer from './reducers/toastReducer';
import initErrorReducer from './reducers/initErrorReducer';
import orderProductReducer from './reducers/orderProductReducer';
import basketReducer from './reducers/basketReducer';
import reviewReducer from './reducers/reviewReducer';

const reducers = combineReducers({
    modalReducer,
    toastReducer,
    initErrorReducer,
    orderProductReducer,
    basketReducer,
    reviewReducer
});

export default reducers;