import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom'


class Home extends Component {

    constructor(props) {
        super(props);
    }
    _onClick() {
        this.setState({ loading: true });
    }
    render() {
        return (
            <div className="home" style={{ textAlign: 'center' }}>
                <a href="/product/1">test routing with param</a>
                <br />
                <p className="text-center" style={{ padding: '50px' }}>this is home</p>
            </div>

        );
    };

}

const mapStateToProps = state => {
    return { ...state.homeReducer };
}

// const mapDispatchToProps = dispatch => ({
// });

export default connect(mapStateToProps, null)(Home);