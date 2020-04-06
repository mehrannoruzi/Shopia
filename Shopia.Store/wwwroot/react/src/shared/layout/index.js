import React from 'react';
import Modal from '../modal';
import { connect } from 'react-redux'
// import CustomHeader from '../header';
import Store from '../../routes/store';
import AboutUs from '../../routes/aboutus';
import Product from '../../routes/product';
import ContactUs from '../../routes/contactus';
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
                    <Route exact path="/:id?"  component={Store}/>
                    <Route path="/aboutus" component={AboutUs}/>
                    <Route path="/contactus" component={ContactUs}/>
                    <Route path="/product/:id" component={Product}/>
                    <Route path="/notFound/:msg?" component={NotFound}/>
                    <Route path="/error/:msg?" component={Error}/>
                </Switch>
                <Modal/>
            </Router>
        );
    }
}

export default connect(null, null)(Layout);
