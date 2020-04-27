import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './assets/css/index.css';
import './assets/css/material-design-iconic-font.min.css';
import 'react-toastify/dist/ReactToastify.css';
import Layout from './shared/layout';
import * as serviceWorker from './serviceWorker';

import { createStore } from 'redux';
import { Provider } from 'react-redux';
import reducers from './redux/reducers';
import { create } from 'jss';
import rtl from 'jss-rtl';
import { MuiThemeProvider, createMuiTheme, StylesProvider, jssPreset } from '@material-ui/core/styles';

const store = createStore(reducers);
const theme = createMuiTheme({
    direction: 'rtl',
});

// Configure JSS
const jss = create({ plugins: [...jssPreset().plugins, rtl()] });

function RTL(props) {
    return (
        <StylesProvider jss={jss}>
            {props.children}
        </StylesProvider>
    );
}

ReactDOM.render(
    <Provider store={store}>
        <RTL>
            <MuiThemeProvider theme={theme}>
                <Layout />
            </MuiThemeProvider>

        </RTL>
    </Provider>, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
