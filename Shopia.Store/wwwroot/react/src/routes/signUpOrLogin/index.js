import React from 'react';
import { Redirect } from "react-router-dom";
import { Carousel, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import productApi from './../../api/productApi';
import Loader from './../../shared/Loader';
import strings from './../../shared/constant';

class SignUpOrLogin extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            notFound: false
        };
    }


    render() {
        return (
            <div>
            </div>
        );
    }
}
// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

// const mapDispatchToProps = dispatch => ({
// });

export default connect(null, null)(SignUpOrLogin);
