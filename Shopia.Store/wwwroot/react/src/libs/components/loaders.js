import React, { Component } from 'react';

export class ThreeDotLoader extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (<span className="three-dot-loader"><span className="dot"></span><span className="dot"></span><span className="dot"></span></span>);
    }
}