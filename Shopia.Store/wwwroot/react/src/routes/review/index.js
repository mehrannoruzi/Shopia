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
import { ChangedBasketItemsAction } from './../../redux/actions/basketAction';
import deliveryCostImage from './../../assets/images/delivery-cost.svg';
import discountImage from './../../assets/images/discount.svg';
import Skeleton from '@material-ui/lab/Skeleton';
import { toast } from 'react-toastify';
import ProductsChangedModal from './comps/ProductsChangedModal';

class Review extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: null,
            totalPrice: 0,
            discount: 0,
            currency: '',
            btnInProgresss: false,
            gatewayUrl: ''
        };
    }

    async componentDidMount() {
        this.props.hideInitError();
        if (this.props.items.length === 0) {
            this.setState(p => ({ ...p, redirect: '/basket' }));
            return;
        }
    }

    async _pay() {
        this.setState(p => ({ ...p, btnInProgresss: true }));
        let rep = await orderSrv.submit(this.props.address, this.props.reciever, this.props.recieverMobileNumber);
        this.setState(p => ({ ...p, btnInProgresss: false }));
        if (rep.success) {
            orderSrv.setOrderId(rep.result.id);
            if (rep.result.basketChanged) {
                this.setState(p => ({ ...p, gatewayUrl: rep.result.url }));
                this.changedProductModal._toggle();
                this.props.changeBasket(rep.result.changedProducts);
            }
            else window.open(rep.result.url, '_self');
        }
        else toast(rep.message, { type: toast.TYPE.ERROR })
    }

    _goToBasket() {
        this.setState(p => ({ ...p, redirect: '/basket' }));
    }

    _continue() {
        window.open(this.state.gatewayUrl, '_self');
    }

    render() {
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
                                        <span className='price'><strong className='val'>{commaThousondSeperator((x.count * x.realPrice).toString())}</strong>{strings.currency}</span>
                                    </div>
                                </div>

                            </Col>
                        </Row>
                    ))}
                    <Row>
                        <Col className='total-wrapper'>
                            <div className='cost m-b'>
                                <img src={deliveryCostImage} alt='delivery' />&nbsp;
                                    <span className='val'>{strings.deliverCost} : {commaThousondSeperator(this.props.deliveryCost.toString())} {strings.currency}</span>
                            </div>

                            <div className='discount m-b'>
                                <img src={discountImage} alt='discount' />&nbsp;
                                    <span className='val'>{strings.discount} : {this.props.totalDiscount} {strings.currency}</span>
                            </div>

                            <div className='price m-b'>
                                <span>{strings.priceToPay} : </span>
                                <span className='val'>{commaThousondSeperator((this.props.deliveryCost + this.props.totalPrice).toString())}</span>
                                <span>{strings.currency}</span>
                            </div>
                        </Col>
                    </Row>
                </Container>
                <button className='btn-next' onClick={this._pay.bind(this)}>
                    {strings.payment}
                    {this.state.btnInProgresss ? <Spinner animation="border" size="sm" /> : null}
                </button>
                <ProductsChangedModal ref={modal => this.changedProductModal = modal} show={this.state.show} continue={this._continue.bind(this)} goToBasket={this._goToBasket.bind(this)} />
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
    changeBasket: (products) => dispatch(ChangedBasketItemsAction(products))
});

export default connect(mapStateToProps, mapDispatchToProps)(Review);
