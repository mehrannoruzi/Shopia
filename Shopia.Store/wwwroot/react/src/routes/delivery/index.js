import React from 'react';
import { connect } from 'react-redux'
import { Link } from 'react-router-dom';
import strings from '../../shared/constant';
import { Alert, Button, Container, Row, Col } from 'react-bootstrap';
import { TextField, Select, MenuItem, FormControl, InputLabel } from '@material-ui/core';

class Delivery extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            message: {
                variant: '',
                text: ''
            },
            time: {
                value: '',
                options: [],
                error: false,
                errorMessage: ''
            },
            day: {
                value: '',
                options: [],
                error: false,
                errorMessage: ''
            },
            discount: {
                value: '',
                error: false,
                errorMessage: ''
            },
            deliveryCost:'',
            totalCost:''
        }
    }

    _inputChanged(e) {
        let state = this.state;
        state[e.target.name].value = e.target.value
        this.setState((p) => ({ ...state }));
    }

    submit(e) {
        if (this.state.username.value !== 'admin' || this.state.password.value !== 'admin') {
            this.setState(p => ({
                ...p,
                message: {
                    variant: 'danger',
                    text: strings.wrongUsernameOrPassword
                }
            }))
            return;
        }
        this.props.logIn('xxx', 1, this.state.username.value);
    }

    render() {
        return (
            <div className="delivery-page">
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
                </Container>

                <div className="form-group">
                    <TextField
                        error={this.state.username.error}
                        id="username"
                        label={strings.username}
                        value={this.state.username.value}
                        onChange={this._inputChanged.bind(this)}
                        helperText={this.state.username.errorMessage}
                        style={{ fontFamily: 'iransans' }}
                        variant="outlined"
                    />
                </div>
                <div className="form-group">
                    <TextField
                        error={this.state.password.error}
                        id="password"
                        type='password'
                        label={strings.password}
                        value={this.state.password.value}
                        onChange={this._inputChanged.bind(this)}
                        helperText={this.state.password.errorMessage}
                        variant="outlined"
                    />
                </div>
                <div className="btn-group">
                    <Button variant="outline-info" onClick={this.submit.bind(this)}>{strings.logIn}</Button>
                </div>
                <div className="recover-password">
                    <Link to="/recoverPassword"><small>{strings.forgotMyPassword}</small></Link>
                </div>
            </div>
        );
    }
}

// const mapStateToProps = state => {
//     return { ...state.headerReducer, ...state.authenticationReducer };
// }

// const mapDispatchToProps = dispatch => ({
//     logIn: (token, userId, username) => { dispatch(LogInAction(token, userId, username)); },
//     showToast: (title, body) => dispatch(ShowToastAction(title, body))
// });

export default connect(null, null)(Delivery);
