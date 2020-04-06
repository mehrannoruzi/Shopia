import React from 'react';

export default class ProductList extends React.Component {

    render() {
        let product = this.props.product;
        return (
            <div className='product'>
                <img src={product.imgUrl} />
            </div>
        );
    }
}
