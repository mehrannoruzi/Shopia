import React from 'react';
import { connect } from 'react-redux';
import { toast } from 'react-toastify';
import { TextField } from '@material-ui/core';
import { Container, Row, Col } from 'react-bootstrap';
import CustomMap from '../../shared/map';
import Header from './../../shared/header';
import Steps from './../../shared/steps';
import strings, { validationStrings } from './../../shared/constant';
import AddressListModal from './comps/addressListModal';
import { Radio, FormControlLabel, RadioGroup } from '@material-ui/core';
import basketSrv from './../../service/basketSrv';
import orderSrv from './../../service/orderSrv';
import { Redirect, Link } from 'react-router-dom';
import { SetAddrssAction } from './../../redux/actions/addressAction';
import { HideInitErrorAction } from './../../redux/actions/InitErrorAction';
import { getCurrentLocation } from './../../shared/utils';

class SelectAddress extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            redirect: false,
            address: {
                value: '',
                error: false,
                message: ''
            },
            recieverMobileNumber: {
                value: '',
                error: false,
                message: ''
            },
            reciever: {
                value: '',
                error: false,
                message: ''
            },
            location: {
                lng: this.props.lng,
                lat: this.props.lat,
                message: null
            },
            prevAddress: null
        };
    }

    _inputChanged(e) {
        let state = this.state;
        if (e.target.name) state[e.target.name].value = e.target.value;
        else state[e.target.id].value = e.target.value;
        this.setState((p) => ({ ...p, ...state }));
    }

    _mapChanged(lng, lat) {
        this.setState(p => ({ ...p, location: { lng: lng, lat: lat } }));
    }

    _selectAddress(item) {
        this.setState(p => ({ ...p, prevAddress: item }));
    }

    _remmoveAddress() {
        this.setState(p => ({ ...p, prevAddress: null }));
    }

    async componentDidMount() {
        let loc = await getCurrentLocation();
        if (loc)
            this.setState(p => ({ ...p, location: { lng: loc.lng, lat: loc.lat } }));
        this.props.hideInitError();
        if (basketSrv.get().length === 0)
            toast(strings.doPurchaseProcessAgain, {
                type: toast.TYPE.INFO,
                onClose: function () {
                    this.setState(p => ({ ...p, redirect: '/basket' }));
                }.bind(this)
            });
        let info = orderSrv.getInfo();
        if (!info || !info.token)
            toast(strings.doPurchaseProcessAgain, {
                type: toast.TYPE.INFO,
                onClose: function () {
                    this.setState(p => ({ ...p, redirect: '/completeInformation' }));
                }.bind(this)
            });
    }

    async _showModal() {
        await this.modal._toggle();
    }

    async _fetchAddresses(){

    }
    
    async _submit() {

        if (!this.state.prevAddress) {
            if (!this.state.address.value) {
                this.setState(p => ({ ...p, address: { ...p.address, error: true, message: validationStrings.required } }))
                return;
            }
            if (!this.props.lng || !this.props.lat) {
                this.setState(p => ({ ...p, location: { ...p.location, message: validationStrings.required } }));
                return;
            }
        }
        if (!this.state.reciever.value) {
            this.setState(p => ({ ...p, reciever: { ...p.reciever, error: true, message: validationStrings.required } }))
            return;
        }
        if (!this.state.recieverMobileNumber.value) {
            this.setState(p => ({ ...p, recieverMobileNumber: { ...p.recieverMobileNumber, error: true, message: validationStrings.required } }))
            return;
        }
        if (!this.state.prevAddress) {
            this.props.setAddress({
                address: this.state.address.value,
                lng: this.props.lng,
                lat: this.props.lat
            },
                this.state.reciever.value,
                this.state.recieverMobileNumber.value
            );
        }
        else {
            this.props.setAddress(this.state.prevAddress,
                this.state.reciever.value,
                this.state.recieverMobileNumber.value
            );
        }
        this.setState(p => ({ ...p, redirect: '/review' }));

    }

    render() {
        if (this.state.redirect) return <Redirect to={this.state.redirect} />;
        return (
            <div className="select-address-page with-header">
                <Header goBack={this.props.history.goBack} />
                <Steps activeStep={1} />
                <Container>
                    {this.state.prevAddress ? (
                        <Row className='m-b'>
                            <Col xs={10}>
                                <RadioGroup aria-label="address" name="old-address" value={this.state.prevAddress.id.toString()}>
                                    <FormControlLabel value={this.state.prevAddress.id.toString()} control={<Radio color="primary" />} label={this.state.prevAddress.address} />
                                </RadioGroup>
                            </Col>
                            <Col xs={2} className='d-flex align-items-center'>
                                <button className='btn-remove-address' onClick={this._remmoveAddress.bind(this)}>
                                    <i className='zmdi zmdi-close'></i>
                                </button>
                            </Col>
                        </Row>

                    ) :
                        (<Row>
                            <Col xs={12} className='m-b'>
                                <Link className={'location-selector ' + (this.state.location.message ? 'error' : '')} to={`/selectLocation/${this.state.location.lng}/${this.state.location.lat}`}>
                                    <CustomMap height='57px' lng={this.props.lng} lat={this.props.lat} hideMarker={true} />
                                    <label>
                                        <span>{strings.selectLocation}</span>
                                        <i className='zmdi zmdi-google-maps'></i>
                                    </label>
                                </Link>
                                <p className='Mui-error'>{this.state.location.message}</p>
                            </Col>
                            <Col xs={12} sm={6}>
                                <div className="form-group">
                                    <TextField
                                        id="address"
                                        error={this.state.address.error}
                                        label={strings.address}
                                        multiline
                                        rows={1}
                                        value={this.state.address.value}
                                        onChange={this._inputChanged.bind(this)}
                                        helperText={this.state.address.message}
                                        variant="outlined" />
                                </div>
                            </Col>
                        </Row>)}

                    <Row>
                        <Col className='d-flex justify-content-end m-b'>
                            <button onClick={this._showModal.bind(this)}>{strings.previouseAddresses}</button>
                        </Col>

                    </Row>
                    <Row>
                        <Col xs={12} sm={6}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.reciever.error}
                                    id="reciever"
                                    label={strings.reciever}
                                    value={this.state.reciever.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.reciever.message}
                                    style={{ fontFamily: 'iransans' }}
                                    variant="outlined" />
                            </div>
                        </Col>
                        <Col xs={12} sm={6}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.recieverMobileNumber.error}
                                    id="recieverMobileNumber"
                                    type='number'
                                    className='ltr-input'
                                    label={strings.mobileNumber}
                                    value={this.state.recieverMobileNumber.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.recieverMobileNumber.message}
                                    style={{ fontFamily: 'iransans' }}
                                    variant="outlined" />
                            </div>
                        </Col>
                    </Row>
                </Container>
                <button className='btn-next' onClick={this._submit.bind(this)}>
                    {strings.continuePurchase}
                </button>
                <AddressListModal ref={(comp) => this.modal = comp} onChange={this._selectAddress.bind(this)} />
            </div >
        );
    }

}
const mapStateToProps = state => {
    return { ...state.mapReducer };
}

const mapDispatchToProps = dispatch => ({
    hideInitError: () => dispatch(HideInitErrorAction()),
    setAddress: (address, reciever, recieverMobileNumber) => dispatch(SetAddrssAction(address, reciever, recieverMobileNumber))
});

export default connect(mapStateToProps, mapDispatchToProps)(SelectAddress);
