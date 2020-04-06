import React from 'react';
import { Link } from 'react-router-dom';

export default class ProductList extends React.Component {

    render() {
        let product = this.props.product;
        return (
            <div className='product'>
                <Link to={`/product/${product.id}`}>
                    <img src={product.imgUrl} alt={product.name} />
                </Link>
            </div>
        );
    }
}
