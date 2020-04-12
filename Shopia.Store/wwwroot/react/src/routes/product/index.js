import React from 'react';
import { Button, Carousel, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { ToggleDrawerAction } from "../../redux/actions/DrawerAction";
import { SendProductInoAction } from "../../redux/actions/sendProductInfoAction";
import Drawer from './../../shared/purchaseDrawer';
import productApi from './../../api/productApi';
import Loader from './../../shared/Loader';
import strings from './../../shared/constant';
import Skeleton from '@material-ui/lab/Skeleton';

class Product extends React.Component {
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
        const { params } = this.props.match;
        let productRep = await productApi.getSingleProduct(params.id);
        if (!productRep.success) {
            return;
        }
        this.props.sendProductIno(productRep.result);
        this.setState(p => ({ ...p, product: { ...productRep.result }, loading: false }));
        //let comp = this;
        //after api call
        // setTimeout(function () {
        //     comp.setState(p => ({ ...p, notFound: true }))
        // }, 2000)
    }
    _purchase() {
        this.props.toggleDrawer();
    }

    render() {
        const p = this.state.product;
        return (
            <div className='product-page'>
                <Carousel>
                    {this.state.product.slides.length === 0 ? (<Skeleton variant='rect' height={360} width='100%' />) :
                        (this.state.product.slides.map((s, idx) => (
                            <Carousel.Item key={idx}>
                                <img className='img-slide' className="d-block w-100" src={s.imgUrl} alt="slide" />
                                <Carousel.Caption>
                                    <h3>{s.Title}</h3>
                                    <p>{s.Desc}</p>
                                </Carousel.Caption>
                            </Carousel.Item>
                        )))}
                </Carousel>
                <Container>
                    <Row className="icons">
                        <Col xs={4} sm={4}>
                            <button className="btn-favorite">
                                <svg aria-label="Save" className="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path d="M43.5 48c-.4 0-.8-.2-1.1-.4L24 29 5.6 47.6c-.4.4-1.1.6-1.6.3-.6-.2-1-.8-1-1.4v-45C3 .7 3.7 0 4.5 0h39c.8 0 1.5.7 1.5 1.5v45c0 .6-.4 1.2-.9 1.4-.2.1-.4.1-.6.1zM24 26c.8 0 1.6.3 2.2.9l15.8 16V3H6v39.9l15.8-16c.6-.6 1.4-.9 2.2-.9z"></path></svg>
                            </button>
                        </Col>

                        <Col xs={8} sm={8} className="col-2">
                            {/* <button className="btn-share">
                            <svg aria-label="Share Post" class="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path d="M46.5 3.5h-45C.6 3.5.2 4.6.8 5.2l16 15.8 5.5 22.8c.2.9 1.4 1 1.8.3L47.4 5c.4-.7-.1-1.5-.9-1.5zm-40.1 3h33.5L19.1 18c-.4.2-.9.1-1.2-.2L6.4 6.5zm17.7 31.8l-4-16.6c-.1-.4.1-.9.5-1.1L41.5 9 24.1 38.3z"></path><path d="M14.7 48.4l2.9-.7"></path></svg>
                        </button> */}
                            <button className="btn-comment">
                                <svg aria-label="Comment" className="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path clipRule="evenodd" d="M47.5 46.1l-2.8-11c1.8-3.3 2.8-7.1 2.8-11.1C47.5 11 37 .5 24 .5S.5 11 .5 24 11 47.5 24 47.5c4 0 7.8-1 11.1-2.8l11 2.8c.8.2 1.6-.6 1.4-1.4zm-3-22.1c0 4-1 7-2.6 10-.2.4-.3.9-.2 1.4l2.1 8.4-8.3-2.1c-.5-.1-1-.1-1.4.2-1.8 1-5.2 2.6-10 2.6-11.4 0-20.6-9.2-20.6-20.5S12.7 3.5 24 3.5 44.5 12.7 44.5 24z" fillRule="evenodd"></path></svg>
                            </button>
                            <button className='btn-like'>
                                <svg aria-label="Like" className="_8-yf5 " fill="#262626" height="24" viewBox="0 0 48 48" width="24"><path clipRule="evenodd" d="M34.3 3.5C27.2 3.5 24 8.8 24 8.8s-3.2-5.3-10.3-5.3C6.4 3.5.5 9.9.5 17.8s6.1 12.4 12.2 17.8c9.2 8.2 9.8 8.9 11.3 8.9s2.1-.7 11.3-8.9c6.2-5.5 12.2-10 12.2-17.8 0-7.9-5.9-14.3-13.2-14.3zm-1 29.8c-5.4 4.8-8.3 7.5-9.3 8.1-1-.7-4.6-3.9-9.3-8.1-5.5-4.9-11.2-9-11.2-15.6 0-6.2 4.6-11.3 10.2-11.3 4.1 0 6.3 2 7.9 4.2 3.6 5.1 1.2 5.1 4.8 0 1.6-2.2 3.8-4.2 7.9-4.2 5.6 0 10.2 5.1 10.2 11.3 0 6.7-5.7 10.8-11.2 15.6z" fillRule="evenodd"></path></svg>
                            </button>
                        </Col>
                    </Row>
                    <Row className="text-box">
                        <Col>
                            <span className="title">
                                {this.state.product.name}
                            </span>
                            <p>
                                {this.state.product.desc}
                            </p>
                        </Col>
                    </Row>
                </Container>

                <Button disabled={this.state.loading} className="btn-purchase" onClick={this._purchase.bind(this)}>{strings.purchase}</Button>
                <Drawer />
            </div>
        );
    }
}
// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

const mapDispatchToProps = dispatch => ({
    toggleDrawer: () => dispatch(ToggleDrawerAction()),
    sendProductIno: (payload) => dispatch(SendProductInoAction(payload))
});

export default connect(null, mapDispatchToProps)(Product);
