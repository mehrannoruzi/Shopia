import React from 'react';
import { Button, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { ShowInitErrorAction } from "../../redux/actions/InitErrorAction";
import productApi from './../../api/productApi';
import strings from './../../shared/constant';
import Skeleton from '@material-ui/lab/Skeleton';
import Slider from './comps/slider';
import Header from './../../shared/header';
import DiscountBadg from './../../shared/discountBadg';

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
    }
    async _fetchData() {
        const { params } = this.props.match;
        let apiRep = await productApi.getSingleProduct(params.id);
        if (apiRep.code != 200) {
            this.props.showInitError(this._fetchData.bind(this));
            return;
        }
        if (!apiRep.success) {
            //TODO:showToast
        }
        this.setState(p => ({ ...p, product: { ...apiRep.result }, loading: false }));
    }

    async componentDidMount() {
        await this._fetchData();
    }

    _plusCount() {
        if (this.state.count === this.state.product.maxCount) return;
        this.setState(p => ({ ...p, count: p.count + 1 }));
    }

    _minusCount() {
        if (this.state.count === 1) return;
        this.setState(p => ({ ...p, count: p.count - 1 }));
    }

    _addToCart() {
        //this.props.toggleDrawer();
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
                        <Col xs={6} sm={6} >
                            <div className='counter'>
                                <button disabled={this.state.loading} className='btn-plus' onClick={this._plusCount.bind(this)}>+</button>
                                <span className='count'>{this.state.count}</span>
                                <button disabled={this.state.loading} className='btn-minus' onClick={this._minusCount.bind(this)}>-</button>
                            </div>
                        </Col>
                        <Col xs={6} sm={6}>

                            {this.state.loading ? [1, 2].map(x => <Skeleton key={x} variant='text' height={20} />) :
                                (<div className='price-wrapper'>
                                    <div>
                                        <span className='price'>{p.price} {p.currency}</span>
                                        <DiscountBadg discount={p.discount} />
                                    </div>
                                    <div className='real-price-wrapper'>
                                        <span className='real-price'>{p.realPrice}</span>
                                        <span>&nbsp;{p.currency}</span>
                                    </div>
                                </div>)}
                        </Col>
                    </Row>
                </Container>

                <Button disabled={this.state.loading} className="btn-purchase" onClick={this._addToCart.bind(this)}>
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
    showInitError: () => dispatch(ShowInitErrorAction()),
    // sendProductIno: (payload) => dispatch(SendProductInoAction(payload))
});

export default connect(null, mapDispatchToProps)(Product);
