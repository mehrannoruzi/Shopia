import React, { Component } from 'react';

class NotFound extends Component {
    render() {
        console.log('not found');
        return (
            <div className="not-found" style={{ textAlign: 'center' }}>
                <p className="text-center" style={{ padding: '50px' }}>this is not Found</p>
            </div>

        );
    };

}

// const mapStateToProps = state => {
//     return { ...state.homeReducer };
// }

// const mapDispatchToProps = dispatch => ({
// });

export default NotFound;