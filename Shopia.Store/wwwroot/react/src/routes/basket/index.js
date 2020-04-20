import React from 'react';
import { Button, Container, Row, Col } from 'react-bootstrap';
import { connect } from 'react-redux';
import { SendProductInoAction } from "../../redux/actions/sendProductInfoAction";
import productApi from './../../api/productApi';
import strings from './../../shared/constant';
import Skeleton from '@material-ui/lab/Skeleton';
import Slider from './comps/slider';

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
        //this.props.toggleDrawer();
    }

    _like() {

    }

    _favorite() {

    }

    render() {
        const p = this.state.product;
        return (
            <div className='basket-page'>

            </div>
        );
    }
}
// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

const mapDispatchToProps = dispatch => ({
    // toggleDrawer: () => dispatch(ToggleDrawerAction()),
    // sendProductIno: (payload) => dispatch(SendProductInoAction(payload))
});

export default connect(null, mapDispatchToProps)(Product);
