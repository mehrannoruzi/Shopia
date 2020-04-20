import React from 'react';
import { Alert, Button, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import Loader from './../../shared/Loader';
import strings from './../../shared/constant';
import { TextField, Select, MenuItem, FormControl, InputLabel } from '@material-ui/core';
import productApi from './../../api/productApi';
import Skeleton from '@material-ui/lab/Skeleton';

class CompleteOrder extends React.Component {
    constructor(props) {
        super(props);
        console.log(this.props.payload);
        this.state = {
            loading: false,
            message: {
                variant: '',
                text: ''
            },
            count: {
                value: '1',
                error: false,
                errorMessage: ''
            },
            desc: {
                value: '',
                error: false,
                errorMessage: ''
            },
            product: {
                name: this.props.payload.name,
                maxCount: this.props.payload.count
            }
        };
    }

    _next() {
        this.props.next();
    }

    _inputChanged(e) {
        let state = this.state;
        if(e.target.id) state[e.target.id].value = e.target.value;
        else state[e.target.name].value = e.target.value;
        this.setState((p) => ({ ...state }));
    }

    render() {
        let possibleCounts = [];
        for (let i = 1; i < 11; i++) {
            if (i <= this.state.product.maxCount)
                possibleCounts.push(i.toString());
        }
        return (
            <div className='order-product-page'>
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
                    </Row>
                    <Row>
                        <Col xs={8} sm={8}>
                            <div className="form-group">
                                <TextField
                                    id="name"
                                    variant='outlined'
                                    disabled
                                    label={`${strings.name} ${strings.product}`}
                                    value={this.state.product.name}
                                    onChange={this._inputChanged.bind(this)}
                                    style={{ fontFamily: 'iransans' }}
                                />
                            </div>
                        </Col>
                        <Col xs={4} sm={4}>
                            <FormControl variant="outlined" className='form-group'>
                                <InputLabel shrink id='count-label' htmlFor="count">{strings.count}</InputLabel>
                                <Select
                                    labelId="count-label"
                                    id="count"
                                    value={this.state.count.value}
                                    onChange={this._inputChanged.bind(this)}
                                    label={strings.count}
                                    inputProps={{
                                        name: 'count',
                                        id: 'count',
                                    }}
                                >
                                    {possibleCounts.map((c, idx) => (<MenuItem key={idx} value={c}>{c}</MenuItem>))}
                                </Select>
                            </FormControl>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <div className='form-group'>
                                <TextField
                                    id="desc"
                                    label={strings.description}
                                    multiline
                                    rows={4}
                                    onChange={this._inputChanged.bind(this)}
                                    value={this.state.desc.value}
                                    variant="outlined"
                                />
                            </div>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <div className="btn-group">
                                <Button variant="outline-info" onClick={this._next.bind(this)}>{strings.setp} {strings.next}</Button>
                            </div>
                        </Col>
                    </Row>
                </Container>
                <Loader show={this.state.loading} />
            </div>
        );
    }
}

const mapStateToProps = (state, ownProps) => {
    return { ...ownProps, ...state.orderProductReducer };
}

// const mapDispatchToProps = dispatch => ({
//     toggleDrawer: () => dispatch(ToggleDrawerAction())
// });

export default connect(mapStateToProps, null)(OrderProduct);