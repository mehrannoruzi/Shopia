import React from 'react';
import { connect } from 'react-redux';
import { Container, Row, Col } from 'react-bootstrap';
import CustomMap from '../../shared/map';
import { SetLocationAction } from './../../redux/actions/mapAction';
import strings from './../../shared/constant';
import Header from './../../shared/header';

class SelectLocation extends React.Component {
    constructor(props) {
        super(props);
        const { params } = this.props.match;
        this.lng = params.lng;
        this.lat = params.lat;
    }

    _mapChanged(lng, lat) {
        console.log('in map');
        console.log(lng+'-'+lat);
        this.lng = lng;
        this.lat = lat;
    }

    _setLocation() {
        this.props.setLocation(this.lng, this.lat);
        this.props.history.goBack();
    }

    render() {
        return (
            <div className="select-location-page with-header">
                <Header hasTitle={true} title={strings.map} goBack={this.props.history.goBack} />
                <CustomMap height='100vh' lng={this.lng} lat={this.lat} onChanged={this._mapChanged.bind(this)} />
                <button className='btn-next' onClick={this._setLocation.bind(this)}>{strings.selectLocation}</button>
            </div>
        );
    }

}

// const mapStateToProps = state => {
//     return { ...state.basketReducer };
// }

const mapDispatchToProps = dispatch => ({
    setLocation: (lat, lng) => dispatch(SetLocationAction(lat, lng))
});

export default connect(null, mapDispatchToProps)(SelectLocation);
