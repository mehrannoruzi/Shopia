import React from 'react';

class Product extends React.Component {
    componentDidMount(){

    }
    render() {
        const { params } = this.props.match;
        return (
            <div className="product">
                <img src='' />
                <p className="text-center" style={{ padding: '50px' }}>this is product</p>
            </div>
        );
    }
}

export default Product;
