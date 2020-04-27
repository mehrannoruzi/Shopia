import React from 'react';
import Product from './product';
import productApi from './../../../api/productApi';
import Skeleton from '@material-ui/lab/Skeleton';
import { arrangeInRows } from './../../../shared/utils';
import { ShowInitErrorAction } from '../../../redux/actions/InitErrorAction';
import { connect } from 'react-redux';

class ProductsList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            products: null,
            empty: true
        }
        this.loading = false;
        this.pageNumber = 1;
        this.pageSize = 12;
        this._isMounted = true;
    }

    async _fetchData() {
        if (this.loading) return;
        this.loading = true;
        let productsRep = await productApi.getProducts(this.props.storeId,
            this.props.category,
            this.pageNumber,
            this.pageSize);
        if (!this._isMounted) return;
        this.loading = false;
        if (productsRep.success) {
            if (productsRep.result.length === 0 && this.pageNumber > 1)
                this.pageNumber--;
            this.setState(p => ({ ...p, products: [...(p.products ? p.products : []), ...productsRep.result] }))
        }
        else this.props.showInitError(this._fetchData.bind(this));


    }

    async componentDidMount() {
        await this._fetchData();
        window.addEventListener("scroll", this._handleOnScroll.bind(this));
    }

    componentWillUnmount() {
        window.removeEventListener("scroll", this._handleOnScroll.bind(this));
        this._isMounted = false;
    }
    async _handleOnScroll() {
        const scrollTop = (document.documentElement && document.documentElement.scrollTop) ||
            document.body.scrollTop;
        const scrollHeight = (document.documentElement && document.documentElement.scrollHeight) ||
            document.body.scrollHeight;
        const clientHeight = document.documentElement.clientHeight || window.innerHeight;
        const scrolledToBottom =
            Math.ceil(scrollTop + clientHeight) + 10 >= scrollHeight;
        if (scrolledToBottom) {
            this.pageNumber++;
            await this._fetchData();
        }
    };
    render() {
        if (this.state.products) return (arrangeInRows(2, this.state.products.map((p, idx) => (<Product key={idx} product={p} />)), 'products-list'));
        else return (arrangeInRows(2, [1, 2, 3, 4, 5, 6].map(x => (<div key={x} className='skeleton-product'><Skeleton variant="rect" /></div>)), 'products-list'));
    }
}

const mapStateToProps = (state, ownProps) => {
    return { ...ownProps };
}

const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData) => dispatch(ShowInitErrorAction(fetchData))
});


export default connect(mapStateToProps, mapDispatchToProps)(ProductsList);