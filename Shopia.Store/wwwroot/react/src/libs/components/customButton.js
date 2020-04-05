import React, { Component } from 'react';
import { mainColors } from './../constant';
import { Spinner } from 'react-bootstrap';

export default class CustomButton extends Component {
    render() {
        let type = 'button';;
        let v = 'primary';
        let style = {};
        let dir = 'rtl';
        if (this.props.dir) dir = this.props.dir;
        if (this.props.type) type = this.props.type;
        if (this.props.variant) v = this.props.variant;
        if (this.props.style) v = this.props.style;
        return (<button type={type}
            disabled={this.props.loading}
            className={`btn-custom ${dir} ${v}`}
            style={{ backgroundColor: mainColors[v].bgColor, color: mainColors[v].textColor ,...style}}
            onClick={this.props.onClick}>
            <span className='text'>{this.props.text}</span>
            <span className='icon'>{this.props.loading ? (<Spinner
                as="span"
                animation="border"
                size="sm"
                role="status"
                aria-hidden="true"
                />) : (<i className={this.props.icon}></i>)}</span>
        </ button>);
    }
}