import React from 'react';
import { Card, Button, Carousel, Container, Row, Col } from 'react-bootstrap';
import addressApi from '../../api/addressApi';
import CustomMap from '../../shared/map';

class ManageAddress extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            addresses: []
        };
    }

    async _fetchData() {
        let callRep = await addressApi.getAddresses();
        if (!callRep.success) {
            //TODO: use toast
            return;
        }
        this.setState(p => ({ ...p, loading: false, addresses: callRep.result }))
    }

    async componentDidMount() {
        await this._fetchData();
    }

    render() {
        return (
            <div className="addresses-list-page">
                <Carousel>
                    {this.state.addresses.map((a, idx) => (
                        <Carousel.Item key={idx}>
                            <CustomMap location={{ lng: a.lng, lat: a.lat }} />
                            <Carousel.Caption>
                                <h3>{a.addrss}</h3>
                            </Carousel.Caption>
                        </Carousel.Item>
                    ))}
                </Carousel>
            </div>
        );
    }

}

export default ManageAddress;
