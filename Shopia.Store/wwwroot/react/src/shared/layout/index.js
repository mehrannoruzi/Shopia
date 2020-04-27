import React from 'react';
import Modal from '../modal';
import { connect } from 'react-redux'
import Store from '../../routes/store';
import ContactUs from '../../routes/contactUs';
import Product from '../../routes/product';
import Basket from './../../routes/basket';
import AfterGateway from '../../routes/afterGateway';
import NotFound from '../../routes/notFound';
import InitError from '../../shared/initError';
import { ToastContainer} from 'react-toastify';
import CompleteInfo from '../../routes/completeInformation';
import SelectAddress from '../../routes/selectAddress';

import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

class Layout extends React.Component {
    render() {
        return (
            <Router className="layout">
                {/* <CustomHeader /> */}
                <Switch>
                    <Route path="/product/:id" component={Product} />
                    <Route path="/basket" component={Basket} />
                    <Route path="/completeInformation" component={CompleteInfo} />
                    <Route path="/selectAddress" component={SelectAddress} />
                    <Route path="/contactus" component={ContactUs} />
                    <Route path="/afterGateway/:orderId" component={AfterGateway} />
                    <Route path="/notFound/:msg?" component={NotFound} />
                    <Route exact path="/:id?" component={Store} />

                </Switch>
                <Modal />
                <InitError />
                <ToastContainer containerId={'common_toast'} rtl />
            </Router>
        );
    }
}

export default connect(null, null)(Layout);
