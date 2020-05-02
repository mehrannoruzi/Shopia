import React from 'react';
import { Spinner, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { Link, Redirect } from 'react-router-dom';
import strings from './../../shared/constant';
import DiscountBadg from './../../shared/discountBadg';
import orderSrv from './../../service/orderSrv';
import Header from './../../shared/header';
import { commaThousondSeperator } from './../../shared/utils';
import addressApi from '../../api/addressApi';
import { ShowInitErrorAction, HideInitErrorAction } from "../../redux/actions/InitErrorAction";
import deliveryCostImage from './../../assets/images/delivery-cost.svg';
import discountImage from './../../assets/images/discount.svg';
import Skeleton from '@material-ui/lab/Skeleton';
import { toast } from 'react-toastify';

class Review extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            redirect: null,
            totalPrice: 0,
            discount: 0,
            cost: 0,
            currency: '',
            btnInProgresss: false
        };
    }

    async _getDeliverCost() {
        let info = orderSrv.getInfo();
        if (!info) {
            return;
        }
        let apiRep = await addressApi.getDeliveryCost(info.token, this.state.address);
        if (!apiRep.success)
            if (!apiRep.success) {
                this.props.showInitError(this._fetchData.bind(this), apiRep.message);
                return;
            }
        this.setState(p => ({ ...p, cost: apiRep.result.cost, loading: false }));
    }

    async componentDidMount() 
    {
        console.log(this.props.address);
        if (this.props.items.length === 0) {
            this.setState(p => ({ ...p, redirect: '/basket' }));
            return;
        }
        this.props.hideInitError();
        await this._getDeliverCost();
    }

    async _pay() {
        this.setState(p => ({ ...p, btnInProgresss: true }));
        let rep = await orderSrv.submit(this.props.address, this.props.reciever, this.props.recieverMobileNumber);
        this.setState(p => ({ ...p, btnInProgresss: false }));
        console.log(rep);
        if (rep.success)
            window.open(rep.result.url,'_self');//.location.href = rep.result;
        else toast(rep.message, { type: toast.TYPE.ERROR })
    }
    render() {
        const p = this.state.product;
        if (this.state.redirect) return <Redirect to={this.state.redirect} />
        return (
            <div className='review-page with-header'>
                <Header goBack={this.props.history.goBack} />
                <Container className='basket-wrapper'>
                    {this.props.items.map((x) => (
                        <Row key={x.id}>
                            <Col>
                                <div className='item'>
                                    {x.imgUrl ?
                                        (<div className='img-wrapper'>
                                            <Link to={`product/${x.id}`}><img src={x.imgUrl} alt='img item' /></Link>
                                        </div>) : null}

                                    <div className='info'>
                                        <div className='name m-b'>
                                            <h2 className='hx'>{x.name}</h2>
                                            <DiscountBadg discount={x.discount} />
                                        </div>
                                        <span className='count m-b'>{strings.count}: {x.count}</span>
                                        <span className='price'><strong className='val'>{commaThousondSeperator((x.count * x.realPrice).toString())}</strong>{x.currency}</span>
                                    </div>
                                </div>

                            </Col>
                        </Row>
                    ))}
                    <Row>
                        <Col className='total-wrapper'>
                            {this.state.loading ? <div className='cost m-b'><Skeleton variant='rect' width={120} height={30} /></div> :
                                (<div className='cost m-b'>
                                    <img src={deliveryCostImage} alt='delivery' />&nbsp;
                                    <span className='val'>{strings.deliverCost} : {this.state.cost} {this.state.currency}</span>
                                </div>)}

                            {this.state.loading ? <div className='discount m-b'><Skeleton variant='rect' width={120} height={30} /></div> :
                                (<div className='discount m-b'>
                                    <img src={discountImage} alt='discount' />&nbsp;
                                    <span className='val'>{strings.discount} : {this.props.items.reduce(function (total, x) {
                                        return total + x.price - x.realPrice;
                                    }, 0)} {this.state.currency}</span>
                                </div>)}

                            {this.state.loading ? <div className='price m-b'><Skeleton variant='rect' width={200} height={30} /></div> :
                                (<div className='price m-b'>
                                    <span>{strings.priceToPay} : </span>
                                    <span className='val'>{this.state.cost + this.props.totalPrice} {this.state.currency}</span>
                                    <span>{this.props.items[0].currency}</span>
                                </div>)}
                        </Col>
                    </Row>
                </Container>
                <button className='btn-next' onClick={this._pay.bind(this)}>
                    {strings.payment}
                    {this.state.btnInProgresss ? <Spinner animation="border" size="sm" /> : null}
                </button>
            </div>
        );
    }
}
const mapStateToProps = state => {
    return { ...state.reviewReducer, ...state.basketReducer };
}

const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData, message) => dispatch(ShowInitErrorAction(fetchData, message)),
    hideInitError: () => dispatch(HideInitErrorAction()),
});

export default connect(mapStateToProps, mapDispatchToProps)(Review);
