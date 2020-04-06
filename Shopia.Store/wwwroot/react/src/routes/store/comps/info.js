import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import strings from './../../../shared/constant';

export default class Info extends React.Component {

    render() {
        const info = this.props.info;
        return (
            <div className="info">
                <Container>
                    <Row className="m-b">
                        <Col className="followers">
                            <span>{info.followingCount}</span>
                            <small>{strings.following}</small>
                        </Col>
                        <Col className="followers">
                            <span>{info.followersCount}</span>
                            <small>{strings.followers}</small>
                        </Col>
                        <Col className="posts">
                            <span>{info.postsCount}</span>
                            <small>{strings.posts}</small>
                        </Col>
                        <Col className="logo"><img src={info.logoUrl} alt='logo' /></Col>
                    </Row>
                    <Row className="details">
                        <Col className="name" xs={12} sm={6}>{info.name}</Col>
                        <Col className="desc" xs={12} sm={6}>{info.desc}</Col>
                        <Col className="sccount-txt" xs={12} sm={6}>{info.accountText}</Col>
                        <Col className="delivery-txt" xs={12} sm={6}>{info.deliveryText}</Col>
                        <Col className="gateway-txt" xs={12}>{info.gatewayText}</Col>
                    </Row>
                </Container>
            </div >
        );
    }
}
