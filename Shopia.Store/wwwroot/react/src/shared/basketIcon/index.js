import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import strings from './../constant';

class BasketIcon extends React.Component {

    render() {
        return (
            <div className='basket-icon-comp'>
                <Link to='/basket'>
                    <i className='icon zmdi zmdi-shopping-basket'></i>
                    <span> {strings.basket}</span>
                    <span> ({this.props.count})</span>
                </Link>
            </div>
        );
    }
}


const mapStateToProps = state => {
    return { ...state.basketIconReducer };
}

// const mapDispatchToProps = dispatch => ({
// });

export default connect(mapStateToProps, null)(BasketIcon);