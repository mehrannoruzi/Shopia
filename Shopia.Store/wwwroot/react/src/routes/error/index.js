import React, { Component } from 'react';

class Error extends Component {
    render() {
        const { params } = this.props.match;
        return (
            <div className="error-page" style={{ textAlign: 'center' }}>
                <p className="text-center" style={{ padding: '50px' }}>{params.msg}</p>
            </div>

        );
    };

}

// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

// const mapDispatchToProps = dispatch => ({
// });

export default Error;