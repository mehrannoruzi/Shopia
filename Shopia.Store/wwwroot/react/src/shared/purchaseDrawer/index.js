import React from 'react';
import { connect } from 'react-redux';
import { Drawer } from '@material-ui/core';
import OrderProduct from './../../routes/orderProduct';
import AddresList from '../../routes/addressesList';
import SingleAddress from './../../routes/singleAddress';
import { ToggleDrawerAction } from '../../redux/actions/DrawerAction';

class PurchaseDrawer extends React.Component {
    constructor(props) {
        super(props);
        this.comps = [{ key: 'orderProduct', comp: OrderProduct },
        { key: 'addressList', comp: AddresList },
        { key: 'singleAddress', comp: SingleAddress }]
        this.state = {
            activeIndex: 0,
            activeComp: this.comps[0].comp,
            payload: this.props.payload
        };
    }
    _changePanel(idx, payload = {}) {
        this.setState(p => ({ ...p, activeComp: this.comps[idx].comp, activeIndex: idx, payload: payload }))
    }
    _go(key, payload = {}) {
        let idx = this.comps.findIndex(x => x.key === key);
        this.setState(p => ({ ...p, activeComp: this.comps[idx].comp, activeIndex: idx, payload: payload }))
    }
    _next(payload = {}) {
        let idx = ++this.state.activeIndex;
        console.log('fired' + idx);
        this.setState(p => ({ ...p, activeComp: this.comps[idx].comp, activeIndex: idx, payload: payload }))
    }
    _prev(payload = {}) {
        let idx = --this.state.activeIndex;
        this.setState(p => ({ ...p, activeComp: this.comps[idx].comp, activeIndex: idx, payload: payload }))
    }
    _toggle() {
        this.props.toggle();
    }
    render() {
        console.log('rendred');
        let Comp = this.state.activeComp;
        return (
            <Drawer anchor='bottom' open={this.props.open} onClose={this._toggle.bind(this)}>
                {<Comp changePanel={this._changePanel.bind(this)}
                    next={this._next.bind(this)}
                    prev={this._prev.bind(this)}
                    go={this._go.bind(this)}
                    payload={this.state.payload} />}
            </Drawer>
        );
    }
}

const mapStateToProps = state => {
    return { ...state.drawerReducer };
}

const mapDispatchToProps = dispatch => ({
    toggle: () => dispatch(ToggleDrawerAction())
});

export default connect(mapStateToProps, mapDispatchToProps)(PurchaseDrawer);