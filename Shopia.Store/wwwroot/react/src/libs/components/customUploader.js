import React, { Component } from 'react';
import { mainColors } from './../constant';
import { Spinner } from 'react-bootstrap';
import CustomButton from './customButton';
import { getDefaultImageUrl, getFileType, fileTypes } from './../utils';

export default class CustomUploader extends Component {
    constructor(props) {
        super(props);
        this.state = {
            uploaded: this.props.uploaded ? this.props.uploaded : false,
            src: this.props.uploaded ? getDefaultImageUrl(this.props.url) : null,
            uploading: false,
            title: this.props.title
        };
    }
    _afterUpload(e) {
        console.log(e.target.files[0]);
        let name = e.target.files[0].name;
        if (getFileType(e.target.files[0].name) !== fileTypes.Image) {
            let src = getDefaultImageUrl(name);
            console.log(getDefaultImageUrl(src));
            this.setState(p => ({ ...p, uploaded: true, src: src, title: name }));
        }
        else {
            let comp = this;
            var reader = new FileReader();
            reader.onload = function (e) {
                comp.setState(p => ({ ...p, uploaded: true, src: e.target.result, title: name }));
            }
            reader.readAsDataURL(e.target.files[0]);
        }

        if (this.props.onChange) this.props.onChange(e.target.files[0]);

    }

    _remove(){
        this.setState(p => ({ ...p, uploaded: false, src: null, title: null }));
        if (this.props.onChange) this.props.onChange(null);
    }
    render() {
        return (<div className='uploader-box'>
            <button onClick={() => this.input.click()}>
                <img className={'img-uploader' + (this.state.uploaded ? '' : 'd-none')} src={this.state.src} />
                <i className={'zmdi zmdi-upload ' + (!this.state.uploaded ? '' : 'd-none')}></i>
                <span className="title">{this.state.title}</span>
            </button>
            <i onClick={this._remove.bind(this)} className={'remove zmdi zmdi-close '+(this.state.uploaded ? '' : 'd-none')}></i>
            <input accept="*" ref={i => this.input = i} onChange={this._afterUpload.bind(this)} className="d-none" type="file" name="uploader" />
        </div>);
    }
}