import React from 'react';
import strings from './../constant';
import commentApi from './../../api/commentApi';

export default class AddComment extends React.Component {
    constructor() {
        this.state = {
            comment: ''
        };
    }
    async _submit() {
        if (!this.comment) {
            return;
        }
        let callRep = await commentApi.addComment(this.props.productId, this.comment.text);
        if(!callRep.success)  {
            return;
        }

    }
    render() {
        return (<div className='add-comment-comp' style={{ display: this.props.show ? 'flex' : 'none' }} >
            <input placeholder={strings.write} id='comment' name='comment' />
            <button onClick={this._submit.bind(this)}>{strings.submit}</button>
        </div>);
    }
}

// const mapStateToProps = (state, ownProps) => {
//     return { ...ownProps, ...state.orderProductReducer };
// }

// const mapDispatchToProps = dispatch => ({
//     toggleDrawer: () => dispatch(ToggleDrawerAction())
// });

export default connect(null, null)(AddComment);