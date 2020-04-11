import React, { Component } from 'react';
import { connect } from 'react-redux';
import ProductsList from './comps/productsList';
import Info from './comps/info';
import Loader from './../../shared/Loader';
import storeApi from '../../api/storeApi';
import { Nav, Tab, Container, Row, Col } from 'react-bootstrap';

class Store extends Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            info: {},
            products: []
        };
    }

    async componentDidMount() {
        this.setState(p => ({ ...p, loading: false }));
        const { params } = this.props.match;
        let storeRep = await storeApi.getSingleStore(params.id);
        if (storeRep.success)
            this.setState(p => ({ ...p, ...storeRep.result, loading: false }));
    }

    render() {
        const { params } = this.props.match;
        return (
            <div className="store-page">
                <Info info={this.state} />
                <Tab.Container id="tabs" defaultActiveKey="productsList">
                    <Nav variant="tabs">
                        <Container>
                            <Row>
                                <Col>
                                    <Nav.Item>
                                        <Nav.Link eventKey="productsList">
                                            <svg aria-label="Posts" className="_8-yf5 " fill="#0095f6" height="24" viewBox="0 0 48 48" width="24"><path clipRule="evenodd" d="M45 1.5H3c-.8 0-1.5.7-1.5 1.5v42c0 .8.7 1.5 1.5 1.5h42c.8 0 1.5-.7 1.5-1.5V3c0-.8-.7-1.5-1.5-1.5zm-40.5 3h11v11h-11v-11zm0 14h11v11h-11v-11zm11 25h-11v-11h11v11zm14 0h-11v-11h11v11zm0-14h-11v-11h11v11zm0-14h-11v-11h11v11zm14 28h-11v-11h11v11zm0-14h-11v-11h11v11zm0-14h-11v-11h11v11z" fillRule="evenodd"></path></svg>
                                        </Nav.Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Nav.Link eventKey="second">Tab 2</Nav.Link>
                                    </Nav.Item>
                                </Col>
                            </Row>
                        </Container>

                    </Nav>


                    <Tab.Content>
                        <Tab.Pane eventKey="productsList" className="products-list">
                            <ProductsList storeId={params.id} />
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

export default connect(mapStateToProps, null)(Store);