import React from 'react';
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
