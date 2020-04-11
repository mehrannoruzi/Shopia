import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import strings from './../../../shared/constant';
import Skeleton from '@material-ui/lab/Skeleton';

export default class Info extends React.Component {

    render() {
        const info = this.props.info;
        console.log(info);
        return (
            <div className="info">
                <Container>
                    <Row className="m-b">
                        <Col className="followers">
                            <span> {info.followingCount ? info.followingCount : <Skeleton variant='text' height={15} width={40} />}</span>
                            <small>{strings.following}</small>
                        </Col>
                        <Col className="followers">
                            <span> {info.followersCount ? info.followersCount : <Skeleton variant='text' height={15} width={40} />}</span>
                            <small>{strings.followers}</small>
                        </Col>
                        <Col className="posts">
                            <span> {info.postsCount ? info.postsCount : <Skeleton variant='text' height={15} width={40} />}</span>
                            <small>{strings.posts}</small>
                        </Col>
                        <Col className="logo">
                            {info.logoUrl ? <img src={info.logoUrl} alt='logo' /> : <Skeleton variant='circle' width={100} height={100} />}
                        </Col>
                    </Row>
                    <Row className="details" dir="ltr" >

                        <Col className="name" xs={12} sm={6}>
                            {info.name ? info.name : <Skeleton variant='text' height={20} width={50} />}
                        </Col>
                        <Col className="desc" xs={12} sm={6}>
                            {info.desc ? info.desc : <Skeleton variant='text' height={20} width={100} />}
                        </Col>
                        <Col className="sccount-txt" xs={12} sm={6}>
                            {info.accountText ? info.accountText : <Skeleton variant='text' height={20} width={100} />}
                        </Col>
                        <Col className="delivery-txt" xs={12} sm={6}>
                            {info.deliveryText ? info.deliveryText : <Skeleton variant='text' height={20} width={150} />}
                        </Col>
                        <Col className="gateway-txt" xs={12}>
                            {info.gatewayText ? info.gatewayText : <Skeleton variant='text' height={20} width={150} />}
                        </Col>
                    </Row>
                </Container>
            </div >
        );
    }
}
