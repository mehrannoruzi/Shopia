import React, { Component } from 'react';
import { connect } from 'react-redux';
import ProductsList from './comps/productsList';
import Info from './comps/info';
import { Nav, Tab, Container, Row, Col } from 'react-bootstrap';
import strings from './../../shared/constant';

class Store extends Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            info: {},
            products: []
        };
    }


    render() {
        const { params } = this.props.match;
        return (
            <div className="store-page">
                <Info id={this.props.match.params.id} />
                <Tab.Container id="tabs" defaultActiveKey="all">
                    <Nav variant="tabs">
                        <Container>
                            <Row>
                                <Col>
                                    <Nav.Item>
                                        <Nav.Link eventKey="all">{strings.all}</Nav.Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Nav.Link eventKey="newests"><i class="icon newests-icon zmdi zmdi-flash"></i> {strings.newests}</Nav.Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Nav.Link eventKey="favorites">{strings.favorites}</Nav.Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Nav.Link eventKey="best-sellers">{strings.bestSellers}</Nav.Link>
                                    </Nav.Item>
                                </Col>
                            </Row>
                        </Container>

                    </Nav>


                    <Tab.Content>
                        <Tab.Pane eventKey="all" className="all">
                            <ProductsList storeId={params.id} category='all' />
                        </Tab.Pane>
                        <Tab.Pane eventKey="newests">
                            <ProductsList storeId={params.id} category='newests' />
                        </Tab.Pane>
                        <Tab.Pane eventKey="favorites">
                            <ProductsList storeId={params.id} category='newests' />
                        </Tab.Pane>
                        <Tab.Pane eventKey="best-sellers">
                            <ProductsList storeId={params.id} category='bestSellers' />
                        </Tab.Pane>
                    </Tab.Content>
                </Tab.Container>
            </div>

        );
    };

}

// const mapStateToProps = state => {
//     return { ...state.initErrorReducer };
// }

// const mapDispatchToProps = dispatch => ({
//     showInitError:()=>dispatch(ShowInitError());
// });

export default connect(null, null)(Store);