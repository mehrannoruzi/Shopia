import React from 'react';
import { Button, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { ShowInitErrorAction, HideInitErrorAction } from "../../redux/actions/InitErrorAction";
import productApi from './../../api/productApi';
import strings from './../../shared/constant';
import Skeleton from '@material-ui/lab/Skeleton';
import Slider from './comps/slider';
import Header from './../../shared/header';
import DiscountBadg from './../../shared/discountBadg';
import { AddToBasketAction } from './../../redux/actions/basketAction';
import basketSrv from './../../service/basketSrv';
import { commaThousondSeperator, checkLocalStorage } from './../../shared/utils';

class Product extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            count: 1,
            product: {
                name: '',
                price: 0,
                maxCount: 0,
                realPrice: 0,
                discount: 0,
                likeCount: 0,
                slides: [],
                desc: ''
            }
        };
        this._isMounted = true;
    }

    async _fetchData() {
        const { params } = this.props.match;
        let apiRep = await productApi.getSingleProduct(params.id);
        console.log(apiRep);
        if (!this._isMounted) return;
        if (!apiRep.success) {
            this.props.showInitError(this._fetchData.bind(this), apiRep.message);
            return;
        }
        this.setState(p => ({ ...p, product: { ...apiRep.result }, loading: false }));
    }

    async componentDidMount() {
        checkLocalStorage();
        this.props.hideInitError();
        await this._fetchData();
    }

    componentWillUnmount() {
        this._isMounted = false;
    }

    _plusCount() {
        if (this.state.count === this.state.product.maxCount) return;
        this.setState(p => ({ ...p, count: p.count + 1 }));
    }

    _minusCount() {
        if (this.state.count === 1) return;
        this.setState(p => ({ ...p, count: p.count - 1 }));
    }

    _addToBasket() {
        basketSrv.add(this.state.product, this.state.count);
        this.props.addToBasket(this.state.product, this.state.count);
    }

    render() {
        const p = this.state.product;
        return (
            <div className='product-page'>
                <Header goBack={this.props.history.goBack} />
                <Slider slides={this.state.product.slides} />
                <Container className='info'>
                    <Row className='name-row'>
                        <Col>
                            {this.state.loading ? <div className='name'><Skeleton variant='rect' height={20} width='60%' /></div> :
                                <h2 className='name'>{p.name}</h2>}
                        </Col>
                    </Row>
                    <Row className="details-row m-b">
                        <Col>
                            {this.state.loading ? [1, 2, 3, 4].map(x => <Skeleton key={x} variant='text' />) :
                                <p className='desc'>{p.desc}</p>}
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={6} sm={6} className='d-flex align-items-center'>
                            <div className='counter'>
                                <button disabled={this.state.loading} className='btn-plus' onClick={this._plusCount.bind(this)}>+</button>
                                <span className='count'>{this.state.count}</span>
                                <button disabled={this.state.loading} className='btn-minus' onClick={this._minusCount.bind(this)}>-</button>
                            </div>
                        </Col>
                        <Col xs={6} sm={6}>

                            {this.state.loading ? [1, 2].map(x => <Skeleton key={x} variant='text' height={20} />) :
                                (<div className='price-wrapper'>
                                    {
                                        p.discount ? (
                                            <div>
                                                <span className='price'>{commaThousondSeperator(p.price.toString())} {p.currency}</span>
                                                <DiscountBadg discount={p.discount} />
                                            </div>) : null
                                    }
                                    <div className='real-price-wrapper'>
                                        <span className='real-price'>{commaThousondSeperator(p.realPrice.toString())}</span>
                                        <span className='currency'>{p.currency}</span>
                                    </div>
                                </div>)}
                        </Col>
                    </Row>
                </Container>

                <Button disabled={this.state.loading} className="btn-purchase" onClick={this._addToBasket.bind(this)}>
                    {`${strings.add} ${strings.to} ${strings.basket}`}
                </Button>
            </div>
        );
    }
}
// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData, message) => dispatch(ShowInitErrorAction(fetchData, message)),
    hideInitError: () => dispatch(HideInitErrorAction()),
    addToBasket: (product, count) => dispatch(AddToBasketAction(product, count))
    // sendProductIno: (payload) => dispatch(SendProductInoAction(payload))
});

export default connect(null, mapDispatchToProps)(Product);
