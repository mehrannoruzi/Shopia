import React from 'react';
import Modal from '../modal';
import CustomToas from '../toast';
import { connect } from 'react-redux'
import Store from '../../routes/store';
import SignUpOrLogin from '../../routes/signUpOrLogin';
import LogIn from '../../routes/logIn';
import SignUp from '../../routes/signUp';
import RecoverPassword from '../../routes/recoverPassword';
import ChangePassword from '../../routes/changePasword';
import Product from '../../routes/product';
import NotFound from '../../routes/notFound';
import Error from '../../routes/error';

import {
    BrowserRouter as Router,
    Switch,
    Route,
} from 'react-router-dom';

class Layout extends React.Component {
    render() {
        return (
            <Router className="layout">
                {/* <CustomHeader /> */}
                <Switch>
                    <Route path="/signUpOrLogin" component={SignUpOrLogin} />
                    <Route path="/logIn" component={LogIn} />
                    <Route path="/signUp" component={SignUp} />
                    <Route path="/recoverPassword" component={RecoverPassword} />
                    <Route path="/changePassword" component={ChangePassword} />
                    <Route path="/product/:id" component={Product} />
                    <Route path="/notFound/:msg?" component={NotFound} />
                    <Route path="/error/:msg?" component={Error} />
                    <Route exact path="/:id?" component={Store} />
                </Switch>
                <Modal />
                <CustomToas />
            </Router>
        );
    }
}

export default connect(null, null)(Layout);
