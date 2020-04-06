import React, { Component } from 'react';
import { connect } from 'react-redux';
import Product from './comps/product';
import Info from './comps/info';
import Loader from './../../shared/Loader';
import storeApi from '../../api/storeApi';
import { Nav, Tab } from 'react-bootstrap';

class Home extends Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            info: {},
            products: []
        };
    }

    async componentDidMount() {
        const { params } = this.props.match;
        let storeRep = await storeApi.getSingleStore(params.id);
        if (storeRep.success)
            this.setState(p => ({ ...p, ...storeRep.result, loading: false }));
    }

    render() {
        return (
            <div className="home-page">
                <Info info={this.state} />
                <Tab.Container id="tabs" defaultActiveKey="productsList">
                    <Nav variant="tabs">
                        <Nav.Item>
                            <Nav.Link eventKey="productsList">
                                <svg aria-label="Posts" className="_8-yf5 " fill="#0095f6" height="24" viewBox="0 0 48 48" width="24"><path clipRule="evenodd" d="M45 1.5H3c-.8 0-1.5.7-1.5 1.5v42c0 .8.7 1.5 1.5 1.5h42c.8 0 1.5-.7 1.5-1.5V3c0-.8-.7-1.5-1.5-1.5zm-40.5 3h11v11h-11v-11zm0 14h11v11h-11v-11zm11 25h-11v-11h11v11zm14 0h-11v-11h11v11zm0-14h-11v-11h11v11zm0-14h-11v-11h11v11zm14 28h-11v-11h11v11zm0-14h-11v-11h11v11zm0-14h-11v-11h11v11z" fillRule="evenodd"></path></svg>
                            </Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link eventKey="second">Tab 2</Nav.Link>
                        </Nav.Item>
                    </Nav>
                    <Tab.Content>
                        <Tab.Pane eventKey="productsList" className="products-list">
                            {this.state.products.map((p, idx) => (<Product key={idx} product={p} />))}
                        </Tab.Pane>
                        <Tab.Pane eventKey="second">
                            222
                        </Tab.Pane>
                    </Tab.Content>
                </Tab.Container>
                <Loader show={this.state.loading} />
            </div>

        );
    };

}

const mapStateToProps = state => {
    return { ...state.homeReducer };
}

// const mapDispatchToProps = dispatch => ({
// });

export default connect(mapStateToProps, null)(Home);