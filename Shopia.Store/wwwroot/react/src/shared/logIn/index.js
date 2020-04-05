import React from 'react';
import { LogInAction } from '../../redux/actions/authenticationAction';
import { HideModalAction } from '../../redux/actions/modalAction';
import { connect } from 'react-redux'

import strings from '../constant';
import { Alert, Button } from 'react-bootstrap';
import { TextField } from '@material-ui/core';

class CustomHeader extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            message: {
                variant: '',
                text: ''
            },
            username: {
                value: '',
                error: false,
                errorMessage: ''
            },
            password: {
                value: '',
                error: false,
                errorMessage: ''
            }
        }
    }

    usernameChanged(e) {
        let v = e.target.value;
        this.setState((p) => ({ ...p, username: { ...p.username, value: v } }));
    }

    passwordChanged(e) {
        let v = e.target.value;
        this.setState((p) => ({ ...p, password: { ...p.password, value:v  } }));
    }

    submit(e) {
        if (this.state.username.value !== 'admin' || this.state.password.value !== 'admin') {
            this.setState(p => ({
                ...p,
                message: {
                    variant:'danger',
                    text:strings.wrongUsernameOrPassword
                }
            }))
            return;
        }
        this.props.logIn('xxx', 1, this.state.username.value);
        this.props.hideModal();
    }

    render() {
        return (
            <div className="login-wrapper">
                <div className="form-group">
                    {
                        this.state.message.variant !== '' ?
                            (<Alert variant={this.state.message.variant}>
                                <p className="text-center">{this.state.message.text}</p>
                            </Alert>) : null
                    }
                </div>
                <div className="form-group">
                    <TextField
                        error={this.state.username.error}
                        id="username"
                        label={strings.username}
                        value={this.state.username.value}
                        onChange={this.usernameChanged.bind(this)}
                        helperText={this.state.username.errorMessage}
                        style={{ fontFamily: 'iransans' }}
                    />
                </div>
                <div className="form-group">
                    <TextField
                        error={this.state.password.error}
                        id="password"
                        type='passwrod'
                        label={strings.password}
                        value={this.state.password.value}
                        onChange={this.passwordChanged.bind(this)}
                        helperText={this.state.password.errorMessage}
                    />
                </div>
                <div className="btn-group">
                    <Button variant="outline-info" onClick={this.submit.bind(this)}>{strings.logIn}</Button>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return { ...state.headerReducer, ...state.authenticationReducer };
}

const mapDispatchToProps = dispatch => ({
    logIn: (token, userId, username) => { dispatch(LogInAction(token, userId, username)); },
    hideModal: () => { dispatch(HideModalAction()); }
});

export default connect(mapStateToProps, mapDispatchToProps)(CustomHeader);
