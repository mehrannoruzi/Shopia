import React from 'react';
import { toast } from 'react-toastify';
import { TextField } from '@material-ui/core';
import { Container, Row, Col } from 'react-bootstrap';
import addressApi from '../../api/addressApi';
import CustomMap from '../../shared/map';
import Header from './../../shared/header';
import Steps from './../../shared/steps';
import strings from './../../shared/constant';
import AddressListModal from './comps/addressListModal';

class SelectAddress extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            address: {
                value: '',
                error: false,
                message: ''
            },
            mobileNumber: {
                value: '',
                error: false,
                message: ''
            },
            fullname: {
                value: '',
                error: false,
                message: ''
            },
            location: {
                lng: 51.337848,
                lat: 35.699858
            },
        };
    }

    _inputChanged(e) {
        let state = this.state;
        if (e.target.name) state[e.target.name].value = e.target.value;
        else state[e.target.id].value = e.target.value;
        this.setState((p) => ({ ...p, ...state }));
    }

    _mapChanged(lng, lat) {

    }

    _selectAddress(id, address) {
        console.log(id, address);
    }

    async _fetchData() {
        let callRep = await addressApi.getAddresses();
        if (!callRep.success) {
            //TODO: use toast
            return;
        }
        this.setState(p => ({ ...p, loading: false, addresses: callRep.result }))
    }
    _submit() {

    }
    async componentDidMount() {
        await this._fetchData();
    }

    _showModal() {
        this.modal._toggle();
    }
    render() {
        return (
            <div className="select-address-page">
                <Header goBack={this.props.history.goBack} />
                <Steps activeStep={1} />
                <Container>
                    <Row>
                        <Col xs={12} sm={6}>
                            <div className="form-group">
                                <TextField
                                    id="address"
                                    label={strings.address}
                                    multiline
                                    rows={1}
                                    value={this.state.address.value}
                                    onChange={this._inputChanged.bind(this)}
                                    variant="outlined" />
                            </div>
                        </Col>
                        <Col xs={12} sm={6}>
                            <CustomMap height={150} location={this.state.location} onChanged={this._mapChanged.bind(this)} />
                        </Col>
                    </Row>
                    <Row>
                        <Col className='d-flex justify-content-end m-b'>
                            <button onClick={this._showModal.bind(this)}>{strings.previouseAddresses}</button>
                        </Col>

                    </Row>
                    <Row>
                        <Col xs={12} sm={6}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.fullname.error}
                                    id="fullname"
                                    label={strings.reciever}
                                    value={this.state.fullname.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.fullname.message}
                                    style={{ fontFamily: 'iransans' }}
                                    variant="outlined" />
                            </div>
                        </Col>
                        <Col xs={12} sm={6}>
                            <div className="form-group">
                                <TextField
                                    error={this.state.mobileNumber.error}
                                    id="mobileNumber"
                                    type='number'
                                    className='ltr-input'
                                    label={strings.mobileNumber}
                                    value={this.state.mobileNumber.value}
                                    onChange={this._inputChanged.bind(this)}
                                    helperText={this.state.mobileNumber.message}
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
            </div>
        );
    }

}

export default SelectAddress;
