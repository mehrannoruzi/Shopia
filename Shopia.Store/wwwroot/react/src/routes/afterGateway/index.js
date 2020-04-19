import React from 'react';
import { Link } from 'react-router-dom';
import strings from '../../shared/constant';
import { Alert, Container, Row, Col } from 'react-bootstrap';
import orderApi from './../../api/orderApi';

export default class AfterGateway extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            message: {
                variant: '',
                text: ''
            },
            deliverDateTime: '',
            traceId: '',
            productCoe: '',
            productCount: 0,

        }
    }

    async componentDidMount() {
        const { params } = this.props.match;
        let callRep = await orderApi.getInfo(params.id);
        if (!callRep.success) {
            return;
        }
    }

    _goBack() {

    }

    render() {
        return (
            <div className="after-gateway-page">
                <Container>
                    <Row>
                        <Col>
                            <div className="form-group">
                                {
                                    this.state.message.variant !== '' ?
                                        (<Alert variant={this.state.message.variant}>
                                            <p className="text-center">{this.state.message.text}</p>
                                        </Alert>) : null
                                }
                            </div>
                        </Col>
                        <Col>
                        </Col>
                        <Col>
                            <div className="btn-group">
                                <Link to="/"><small>{strings.return}</small></Link>
                            </div>
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}
