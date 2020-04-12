import React from 'react';
import Modal from '../modal';
import CustomToas from '../toast';
import { connect } from 'react-redux'
import Store from '../../routes/store';
import Product from '../../routes/product';
import NotFound from '../../routes/notFound';
import Error from '../../routes/error';
import SignUpOrLogin from '../../routes/signUpOrLogin';
import LogIn from '../../routes/logIn';
import SignUp from '../../routes/signUp';
import RecoverPassword from '../../routes/recoverPassword';
import ChangePassword from '../../routes/changePasword';
import SingleAddress from '../../routes/singleAddress';
import OrderProduct from '../../routes/orderProduct';

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
                    <Route path="/product/:id" component={Product} />
                    <Route path="/error/:msg?" component={Error} />
                    <Route path="/notFound/:msg?" component={NotFound} />
                    <Route exact path="/:id?" component={Store} />
                    {/* <Route path="/logIn" component={LogIn} />
                    <Route path="/signUpOrLogin" component={SignUpOrLogin} />
                    <Route path="/signUp" component={SignUp} />
                    <Route path="/recoverPassword" component={RecoverPassword} />
                    <Route path="/changePassword" component={ChangePassword} />
                    <Route path="/singleAddress/:id?" component={SingleAddress} />
                    <Route exact path="/orderProduct/:name/:count" component={OrderProduct} /> */}
                </Switch>
                <Modal />
                <CustomToas />
            </Router>
        );
    }
}

export default connect(null, null)(Layout);
