import React from 'react';
import { Modal, Button } from 'react-bootstrap';
import Skeleton from '@material-ui/lab/Skeleton';
import strings from './../../../shared/constant';
import addressApi from './../../../api/addressApi';
import { ShowInitErrorAction } from '../../../redux/actions/InitErrorAction';
import { connect } from 'react-redux';
import { Radio, FormControlLabel, RadioGroup } from '@material-ui/core';
import orderSrv from './../../../service/orderSrv';

class AddressListModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            show: false,
            disabled: true,
            id: '0',
            items: []
        };
        this._isMounted = true;
    }
    async _fetchData() {
        let info = orderSrv.getInfo();
        if (!info) {
            this.setState(p => ({ ...p, loading: false }));
            return;
        }
        let apiRep = await addressApi.getAddresses(info.token);
        if (!this._isMounted) return;
        if (apiRep.success) this.setState(p => ({
            ...p,
            items: [...apiRep.result],
            id: apiRep.result.length > 0 ? apiRep.result[0].id.toString() : '0',
            disabled: apiRep.result.length > 0 ? false : true,
            loading: false
        }));
        else this.props.showInitError(this._fetchData.bind(this));
    }

    async componentDidMount() {
        await this._fetchData();
    }
    componentWillUnmount() {
        this._isMounted = false;
    }
    _toggle() {
        this.setState(p => ({ ...p, show: !p.show }))
    }
    _onChange(e) {
        let val = e.target.value;
        this.setState(p => ({ ...p, id: val }));
    }

    _select() {
        let item = this.state.items.find(x => x.id == this.state.id);
        this.props.onChange(item);
        this._toggle();
    }
    render() {
        return (
            <Modal
                show={this.state.show}
                onHide={() => this._toggle()}
                centered
                size='lg'
                dialogClassName="modal-90w"
                aria-labelledby="modal-title"
                className='address-list-modal'
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        {strings.previouseAddresses}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {this.state.loading ? [0, 1, 2].map((x) => <Skeleton key={x} variant='rect' height={30} />) :
                        (this.state.items.length === 0 ? (<div className='empty-list'>
                            <i className='zmdi zmdi-mood-bad'></i>
                            <span>{strings.thereIsNoList}</span>
                        </div>) :
                            (<RadioGroup aria-label="address" name="old-address" value={this.state.id.toString()} onChange={this._onChange.bind(this)}>
                                {this.state.items.map((x) => <FormControlLabel key={x.id} className='m-b' value={x.id.toString()} control={<Radio color="primary" />} label={x.address} />)}
                            </RadioGroup>))
                    }
                </Modal.Body>
                <Modal.Footer>
                    <Button disabled={this.state.disabled} variant="primary" className='btn-select' onClick={this._select.bind(this)}>
                        {strings.selectAsAddress}
                    </Button>
                </Modal.Footer>
            </Modal>
        );
    }
}

const mapStateToProps = (state, ownProps) => {
    return { ...ownProps };
}

const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData) => dispatch(ShowInitErrorAction(fetchData))
});

export default connect(mapStateToProps, mapDispatchToProps, null, { forwardRef: true })(AddressListModal);