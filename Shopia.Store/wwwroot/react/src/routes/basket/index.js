import React from 'react';
import { Button, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import basketSrv from './../../service/basketSrv';
import strings from './../../shared/constant';
import DiscountBadg from './../../shared/discountBadg';
import Counter from './../../shared/counter';
import Header from './../../shared/header';
import { commaThousondSeperator } from './../../shared/utils';
import { UpdateBasketAction, RemoveFromBasketAction } from './../../redux/actions/basketAction';
import ConfirmModal from './../../shared/confirm';

class Basket extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            product: {
                name: '',
                price: 0,
                likeCount: 0,
                slides: [],
                desc: ''
            }
        };
    }

    async componentDidMount() {

    }

    _changeCount(id, count) {
        let rep = basketSrv.updateCount(id, count);
        this.props.updateBasket(id, count);
    }

    _delete(id, name) {
        this.modal._toggle(id, strings.areYouSureForDeleteingProduct.replace('##name##', name));

    }
    _confirmDelete(id) {
        console.log(id);
        basketSrv.remove(id);
        this.props.removeFromBasket(id);
    }
    render() {
        const p = this.state.product;
        if (this.props.items.length == 0)
            return (<div className='basket-page'>
                <i className='zmdi zmdi-mood-bad'></i>
            </div>);
        return (
            <div className='basket-page'>
                <Header goBack={this.props.history.goBack} />
                <Container className='basket-wrapper'>
                    {this.props.items.map((x) => (
                        <Row key={x.id}>
                            <Col>
                                <div className='item'>
                                    {x.slides.length !== 0 ? (
                                        <div className='img-wrapper'>
                                            <img src={x.slides[0].imgUrl} alt='img item' />
                                        </div>) : null}

                                    <div className='info'>
                                        <div className='name m-b'>
                                            <h2 className='hx'>{x.name}</h2>
                                            <DiscountBadg discount={x.discount} />
                                        </div>
                                        <Counter id={x.id} className='m-b' count={x.count} max={x.maxCount} onChange={this._changeCount.bind(this)} />
                                        <div className='price-wrapper'>
                                            <span className='price'>{commaThousondSeperator((x.price * x.count).toString())}</span>
                                            <button onClick={this._delete.bind(this, x.id, x.name)} className='btn-delete'><i className='zmdi zmdi-delete'></i></button>
                                        </div>
                                    </div>
                                </div>

                            </Col>
                        </Row>
                    ))}
                </Container>
                <div className='footer-row'>
                    <Container className='total-price-wrapper'>
                        <Row>
                            <Col className='d-flex align-items-center justify-content-end'>
                                <small>{strings.totalSum}:&nbsp;</small>
                                <span className='total-price'>
                                    {commaThousondSeperator(this.props.items.reduce((p, c) => (p + c.price * c.count), 0).toString())}
                                </span>
                                <small>&nbsp;{this.props.items[0].currency}</small>

                            </Col>
                        </Row>
                    </Container>
                    <button className='btn-next'>
                        <span>{strings.continuePurchase}</span>
                    </button>
                </div>
                <ConfirmModal ref={(i) => this.modal = i} onDelete={this._confirmDelete.bind(this)} />
            </div>
        );
    }
}
const mapStateToProps = state => {
    return { ...state.basketReducer };
}

const mapDispatchToProps = dispatch => ({
    updateBasket: (id, count) => dispatch(UpdateBasketAction(id, count)),
    removeFromBasket: (id) => dispatch(RemoveFromBasketAction(id))
});

export default connect(mapStateToProps, mapDispatchToProps)(Basket);
