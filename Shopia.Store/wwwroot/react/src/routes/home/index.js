import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom'
import CustomButton from './../../libs/components/customButton';
import CustomUploader from './../../libs/components/customUploader';

class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { loading: false };
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