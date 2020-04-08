import React from 'react';
import productApi from './../../../api/productApi';
import Product from './product';
import { loaderImage } from './../../../shared/constant';
export default class ProductsList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            products: []
        }
        this.loading = false;
        this.pageNumber = 1;
        this.pageSize = 12;
    }

    async _fetchData() {
        if (this.loading) return;
        this.loading = true;
        let productsRep = await productApi.getProducts(this.props.storeId, this.pageNumber, this.pageSize);
        this.loading = false;
        if (productsRep.success) {
            if (productsRep.result.length === 0 && this.pageNumber > 1)
                this.pageNumber--;
            this.setState(p => ({ ...p, products: [...p.products, ...productsRep.result] }))
        }

    }

    async componentDidMount() {
        await this._fetchData();
        window.addEventListener("scroll", this._handleOnScroll.bind(this));
    }

    componentWillUnmount() {
        window.removeEventListener("scroll", this._handleOnScroll.bind(this));
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
        return (
            <div className='products-list' id='products-list'>
                {this.state.products.length !== 0 ? this.state.products.map((p, idx) => (<Product key={idx} product={p} />)) :
                    (<img className='img-loader' src={loaderImage} />)}
            </div>
        );
    }
}
