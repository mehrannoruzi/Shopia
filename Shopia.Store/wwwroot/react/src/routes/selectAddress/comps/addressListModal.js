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
        if (apiRep.success) this.setState(p => ({ ...p, items: [...apiRep.result], loading: false }));
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
    _onChange(e){
        console.log(e);
        //this.props.onChange();
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
                className='confirm-modal'
            >
                <Modal.Header closeButton>
                    <Modal.Title>
                        {strings.previouseAddresses}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {this.state.loading ? [0, 1, 2].map((x) => <Skeleton key={x} variant='rect' height={30} />) :
                        (this.state.items.length === 0 ? (<div></div>) :
                            (<RadioGroup aria-label="address" name="old-address" value={this.state.items[0].id} onChange={this._onChange.bind(this)}>
                                {this.state.items.map((x) => <FormControlLabel key={x.id} className='m-b' value={x.id} control={<Radio color="primary" />} label={x.address} />)}
                            </RadioGroup>))
                    }
                </Modal.Body>
                {/* <Modal.Footer>
                    <Button variant="danger" onClick={this._delete.bind(this)}>
                        {strings.delete}
                    </Button>
                    <Button variant="secondary" onClick={this._toggle.bind(this)}>
                        {strings.cancel}
                    </Button>
                </Modal.Footer> */}
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