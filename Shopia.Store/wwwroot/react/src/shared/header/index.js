import React from 'react';
import { ShowModalAction } from '../../redux/actions/modalAction';
import { LogOutAction } from '../../redux/actions/authenticationAction';
import { connect } from 'react-redux';
import { Dropdown } from 'react-bootstrap';
import Login from '../logIn';
import strings from '../constant';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

var logedIn = function (username, logOut) {
    return (<div className="loged-in-wrapper">
        <Dropdown>
            <Dropdown.Toggle variant="light" id="dropdown-basic">
                <span className="username">{username}</span>
            </Dropdown.Toggle>

            <Dropdown.Menu>
                <Dropdown.Item dir="rtl" onClick={() => { logOut(); }}>{strings.logOut}</Dropdown.Item>
            </Dropdown.Menu>
        </Dropdown>
    </div>);
}

class CustomHeader extends React.Component {
    showModal() {
        this.props.showModalAction(`${strings.logIn} | ${strings.signUp}`, Login);
    }
    render() {
        return (
            <header className="header">
                <Container>
                    <Row>
                        <Col sm={8} xl={8}>
                            <nav>
                                <ul>
                                    <li>
                                        <Link to="/">{strings.home}</Link>
                                    </li>
                                    <li>
                                        <Link to="/aboutus">{strings.aboutus}</Link>
                                    </li>
                                    <li>
                                        <Link to="/contactus">{strings.contactus}</Link>
                                    </li>
                                </ul>
                            </nav>
                        </Col>
                        <Col sm={4} xl={4} className="account">
                            {this.props.token !== null ? logedIn(this.props.username, this.props.logOutAction) : (<Button variant="outline-primary" onClick={this.showModal.bind(this)}>{strings.logIn} | {strings.signUp}</Button>)}
                        </Col>
                    </Row>
                </Container>

            </header>
        );
    }
}

const mapStateToProps = state => {
    return { ...state.authenticationReducer, ...state.modalReducer };
}

const mapDispatchToProps = dispatch => ({
    showModalAction: (title, body) => { dispatch(ShowModalAction(title, body)); },
    logOutAction: () => dispatch(LogOutAction())
});

export default connect(mapStateToProps, mapDispatchToProps)(CustomHeader);
