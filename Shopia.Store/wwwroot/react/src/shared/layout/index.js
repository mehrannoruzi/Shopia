import React from 'react';
import Modal from '../modal';
import CustomToas from '../toast';
import { connect } from 'react-redux'
import Store from '../../routes/store';
import Product from '../../routes/product';
import AfterGateway from '../../routes/afterGateway';
import NotFound from '../../routes/notFound';
import InitError from '../../shared/initError';

import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

class Layout extends React.Component {
    render() {
        return (
            <Router className="layout">
                {/* <CustomHeader /> */}
                <Switch>
                    <Route path="/product/:id" component={Product} />
                    <Route path="/afterGateway/:id" component={AfterGateway} />
                    <Route path="/notFound/:msg?" component={NotFound} />
                    <Route exact path="/:id?" component={Store} />

                </Switch>
                <Modal />
                <InitError />
            </Router>
        );
    }
}

export default connect(null, null)(Layout);
