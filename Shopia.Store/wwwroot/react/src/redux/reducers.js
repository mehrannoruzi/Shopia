import { combineReducers } from 'redux';
import homeReducer from './reducers/homeReducer';
import authenticationReducer from './reducers/authenticationReducer';
import modalReducer from './reducers/modalReducer';

const reducers = combineReducers({
    authenticationReducer,
    homeReducer,
    modalReducer
});

export default reducers;