import React from 'react';
import { Card, Button, Carousel, Container, Row, Col } from 'react-bootstrap';
import addressApi from './../../api/addressApi';
class  ManageAddress extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            loading:false,
            addresses:[]
        };
    }
    render() {
        return (
            <div className="address-list">
                <Carousel>
                    {this.state.addresses.map(a => (
                        <Carousel.Item>
                            <img className="d-block w-100" src={s.imgUrl} alt="slide" />
                            <Carousel.Caption>
                                <h3>{s.Title}</h3>
                                <p>{s.Desc}</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    ))}
                </Carousel>
            </div>

    );
    }

}

export default ManageAddress;
