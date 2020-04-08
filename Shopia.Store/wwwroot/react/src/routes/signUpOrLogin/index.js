import React from 'react';
import { Redirect } from "react-router-dom";
import { Carousel, Container, Row, Col, Button } from 'react-bootstrap';
import { connect } from 'react-redux';
import productApi from './../../api/productApi';
import Loader from './../../shared/Loader';
import strings from './../../shared/constant';
import accountImage from './../../assets/images/account.png';
import { Link } from 'react-router-dom';
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
            <div className="signup-or-login-page">
                <div className="row-img">
                    <img src={accountImage} alt="account" />
                </div>
                <div className="row-btns">
                    <Link to="/SignUp">
                        <Button varint="outline-info">
                            <span>{strings.signUp}</span>
                        </Button>
                    </Link>

                    <br />
                    <Link to="/LogIn">
                        <Button varint="outline-primary">
                            <span>{strings.loginToSystem}</span>
                        </Button>
                    </Link>

                </div>
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
