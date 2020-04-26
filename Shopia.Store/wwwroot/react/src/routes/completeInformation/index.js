import React from 'react';
import { connect } from 'react-redux'
import { Redirect } from 'react-router-dom';
import strings, { validationStrings } from '../../shared/constant';
import { Container, Row, Col } from 'react-bootstrap';
import { TextField } from '@material-ui/core';
import Header from './../../shared/header';
import Steps from './../../shared/steps';
import orderSrv from './../../service/orderSrv';
import { toast } from 'react-toastify';

class Completeinformation extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: false,
            loading: true,
            message: {
                variant: '',
                text: ''
            },
            fullname: {
                value: '',
                error: false,
                message: ''
            },
            mobileNumber: {
                value: '',
                error: false,
                message: ''
            },
            description: {
                value: '',
                error: false,
                message: ''
            }
        }
    }

    _inputChanged(e) {
        let state = this.state;
        if (e.target.name) state[e.target.name].value = e.target.value;
        else state[e.target.id].value = e.target.value;
        this.setState((p) => ({ ...p, ...state }));
    }

    componentDidMount() {
        let info = orderSrv.getInfo();
        console.log(info);
        if (info != null) {
            this.setState(p => ({
                ...p,
                fullname: { ...p.fullname, value: info.fullname },
                mobileNumber: { ...p.mobileNumber, value: info.mobileNumber },
                description: { ...p.description, value: info.description }
            }));
        }
    }

    async _submit() {
        if (!this.state.fullname.value) {
            this.setState(p => ({ ...p, fullname: { ...p.fullname, error: true, message: validationStrings.required } }));
            return;
        }
        if (!this.state.mobileNumber.value) {
            this.setState(p => ({ ...p, mobileNumber: { ...p.mobileNumber, error: true, message: validationStrings.required } }));
            return;
        }
        if (!this.state.description.value) {
            this.setState(p => ({ ...p, mobileNumber: { ...p.mobileNumber, error: true, message: validationStrings.required } }));
            return;
        }

        let rep = await orderSrv.addInfo({
            fullname: this.state.fullname.value,
            mobileNumber: this.state.mobileNumber.value,
            description: this.state.description.value
        });
        if (!rep.success)
            toast(rep.message, { type: toast.TYPE.DANGER });
        else
            this.setState(p => ({ redirect: true }));

    }

    render() {
        if (this.state.redirect)
            return (<Redirect to='/addressesList' />);
        return (
            <div className="complete-information-page">
                <Header hasTitle={true} goBack={this.props.history.goBack} />
                <Steps />
                <Container>
                    <Row>
                        <Col xs={12}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.fullname.error}
                                    id="fullname"
                                    label={strings.fullname}
                                    value={this.state.fullname.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.fullname.message}
                                    style={{ fontFamily: 'iransans' }}
                                    variant="outlined" />
                            </div>

                        </Col>
                        <Col xs={12}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.mobileNumber.error}
                                    id="mobileNumber"
                                    className='ltr-input'
                                    label={strings.mobileNumber}
                                    value={this.state.mobileNumber.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.mobileNumber.message}
                                    style={{ fontFamily: 'iransans' }}
                                    variant="outlined" />
                            </div>
                        </Col>

                        <Col xs={12}>
                            <div className="form-group">
                                <TextField
                                    id="description"
                                    label={strings.description}
                                    multiline
                                    rows={4}
                                    value={this.state.description.value}
                                    onChange={this._inputChanged.bind(this)}
                                    variant="outlined" />
                            </div>
                        </Col>

                    </Row>

                </Container>

                <button className='btn-next' onClick={this._submit.bind(this)}>
                    {strings.continuePurchase}
                </button>
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

export default connect(null, null)(Completeinformation);
