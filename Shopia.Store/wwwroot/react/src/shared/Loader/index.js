import React from 'react';
import { ShowModalAction } from '../../redux/actions/modalAction';
import { connect } from 'react-redux';
import instagramImage from './../../assets/images/instagram.png';

export default class Loader extends React.Component {

    render() {
        return (
            <div className={"page-loader " + (this.props.show ? "" : "d-none")}>
                <img src={instagramImage} alt="instagram image" />
            </div>
        );
    }
}
