import React from 'react';
import strings from '../../shared/constant';
import { Container, Row, Col } from 'react-bootstrap';
import generalApi from './../../api/generalApi';
import Skeleton from '@material-ui/lab/Skeleton';
import whatsappImage from './../../assets/images/whatsapp.png';
import telegramImage from './../../assets/images/telegram.png';
import { ShowInitErrorAction } from '../../redux/actions/InitErrorAction';
import { connect } from 'react-redux';
import Header from './../../shared/header';

class ConactUs extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
        }
    }

    async _fetchData() {
        let info = await generalApi.getContactUsInfo();
        if (info.success)
            this.setState(p => ({ ...p, ...info.result, loading: false }));
        else this.props.showInitError(this._fetchData.bind(this));
    }

    async componentDidMount() {
        await this._fetchData();
    }

    render() {
        return (
            <div className="contact-us-page">
                <Header title={strings.contactus} goBack={this.props.history.goBack} />
                <Container>
                    <Row className='socials'>
                        <Col xs={6} sm={6}>
                            {this.state.loading ? <Skeleton className='rounded' height={40} width={130} variant='rect' /> :
                                <a className='a-whatsapp' href={this.state.whatsappLink}>
                                    Whats App
                                    <img src={whatsappImage} alt='whatsapp' />
                                </a>}
                        </Col>
                        <Col xs={6} sm={6}>
                            {this.state.loading ? <Skeleton className='rounded' height={40} width={130} variant='rect' /> :
                                <a className='a-telegram' href={this.state.telegramLink}>
                                    Telegram
                                    <img src={telegramImage} alt='telegram' />
                                </a>}
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={6} sm={6} className='website'>
                            <label className='m-b'>{strings.website}</label>
                            {this.state.loading ? <Skeleton variant='text' /> : <a href={this.state.websiteUrl}>{this.state.websiteName}</a>}
                        </Col>
                        <Col xs={6} sm={6} className='phone-numbers'>
                            <label className='m-b'>{strings.phoneNumbers}</label>
                            {this.state.loading ? [1, 2].map(x => <Skeleton key={x} variant='text' />) :
                                this.state.phoneNumbers.map((x, idx) => (<label key={idx}>{x}</label>))}
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}


const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData) => dispatch(ShowInitErrorAction(fetchData))
});

export default connect(null, mapDispatchToProps)(ConactUs);