import React, { Component } from 'react'
// import Mapir from 'mapir-react-component';
import markerImage from './../../assets/images/marker.png';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet'
import L from 'leaflet';
const marker = L.icon({
  iconUrl: markerImage,
  iconSize: [30,30],
  iconAnchor:   [15, 30]
});

export default class CustomMap extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      coords: {
        lng: this.props.location ? this.props.location.lng : 51.337848,
        lat: this.props.location ? this.props.location.lat : 35.699858
      },
      zoom: 13
    }
  }

  _onClick(x) {
    let p = x.latlng;
    console.log(p);
    this.setState(prev => ({
      ...prev,
      coords: {
        lng: p.lng,
        lat: p.lat
      },
    }));
    if (this.props.onChange)
      if (this.props.onChanged)
        this.props.onChanged(p.lng, p.lat);
  }

  _setLocation(lng, lat) {
    this._onClick({ lng: lng, lat: lat });
  }

  _setUserLocation = (p) => {
    if (!navigator.geolocation)
      return;
    navigator.geolocation.getCurrentPosition(position => {
      this.setState(p => ({
        ...p,
        coords: {
          lng: position.coords.longitude,
          lat: position.coords.latitude
        },
      }));
    });
  };
  componentDidMount() {
    //this._setUserLocation();
    this.setState(p => ({ ...p, loading: false }));
    console.log(this.props.location);
  }
  render() {

    return (<Map
      center={[this.state.coords.lat, this.state.coords.lng]}
      zoom={this.state.zoom}
      style={{ height: this.props.height?this.props.height:'200px',width: '100%', overflow: 'hidden' }}
      zoomControl={true}
      doubleClickZoom={true}
      scrollWheelZoom={true}
      dragging={true}
      animate={true}
      onClick={this._onClick.bind(this)}
      easeLinearity={0.35}>
      <TileLayer
        url='http://{s}.tile.osm.org/{z}/{x}/{y}.png'
      />
      <Marker position={[this.state.coords.lat, this.state.coords.lng]}  icon={marker}>
        {/* <img src={markerImage}/> */}
      </Marker>
    </Map>);
  }
}