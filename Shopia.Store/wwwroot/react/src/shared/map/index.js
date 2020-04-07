import React, { Component } from 'react'
import Mapir from 'mapir-react-component';
import markerImage from './../../assets/images/marker.png';
const Map = Mapir.setToken({
  transformRequest: (url) => {
    return {
      url: url,
      headers: {
        'x-api-key': 'eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjQwMjBmN2NmMTIwYWVjZjhiMmQ4OTI2OTZhMjFkNGFiNGRmMDk2OTAxMGQ5ZGEzMTkwNzVjZTRmMTk3OTIwMzc3MzZjMjdhZjk5YWZiMjljIn0.eyJhdWQiOiI4NjA5IiwianRpIjoiNDAyMGY3Y2YxMjBhZWNmOGIyZDg5MjY5NmEyMWQ0YWI0ZGYwOTY5MDEwZDlkYTMxOTA3NWNlNGYxOTc5MjAzNzczNmMyN2FmOTlhZmIyOWMiLCJpYXQiOjE1ODYyNDk0OTksIm5iZiI6MTU4NjI0OTQ5OSwiZXhwIjoxNTg4ODQxNDk5LCJzdWIiOiIiLCJzY29wZXMiOlsiYmFzaWMiXX0.OyA_WsGXMkyoZDvf7xBSHHrqD7aEzeMV71Mibp1gFOqNm2MrRJBqm1QBg81bMyq6RJCW2giWJS9ZDU0m0DPyaRan1-fE8RQ7qI-cFbr-KUCzbDzfHuYSWz4ndn-TF4AIW9xW5gvwXOIGGmAoXz5etw0FEYqFJ5UobOLigpM6bMvHjDDwpPHHro8eCp2CTPZYMZFZuPwAGI0Wpz51m2fUnYe0R15gwZEg3M9p5MLXnXa3IcP78P_cpaepdDQ11Jz9CFqib1tj5gQzc9Xry-PLuB1vXx2FOleSHiRJkyPgoFUa-v0bNfimpZq2x_VeNdesOnWCxcWTfPpUjpFaZYGCnw', //Mapir api key     
        'Mapir-SDK': 'reactjs'
      },
    }
  }
});

export default class CustomMap extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      coords: {
        lng: this.props.init ? this.props.init.lng : 51.338030,
        lat: this.props.init ? this.props.init.lat : 35.700084
      }
    }
  }

  _onClick(p) {
    console.log(p.lngLat);
    this.setState(prev => ({
      ...prev,
      coords: {
        lng: p.lngLat.lng,
        lat: p.lngLat.lat
      },
    }));
    if( this.props.onChange)
    this.props.onChange(p.lngLat.lng, p.lngLat.lat);
  }

  _setUserLocation = (p) => {
    //if (!navigator.geolocation)
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
    this._setUserLocation();
  }
  render() {
    return (
      <Mapir
        width='100%'
        style={{ width: '100px' }}
        onClick={(e, p) => this._onClick(p)}
        center={[this.state.coords.lng, this.state.coords.lat]}
        movingMethod='jumpTo'
        minZoom={[13]}
        //zoom={[14]}
        //Image={markerImage}
        Map={Map}>
        {/* <Mapir.Layer
          type="symbol"
          layout={{ "icon-image": "harbor-15" }}>
        </Mapir.Layer> */}
        <Mapir.Marker
          coordinates={[this.state.coords.lng, this.state.coords.lat]}
          anchor="bottom">
            <img src={markerImage} style={{height:'30px'}}/>
        </Mapir.Marker>
      </Mapir>
    );
  }
}