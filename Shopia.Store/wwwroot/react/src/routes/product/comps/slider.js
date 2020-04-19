import React from 'react';
import {Carousel} from 'react-bootstrap';
import Skeleton from '@material-ui/lab/Skeleton';

export default class Slider extends React.Component {
    render() {
        return (
            <Carousel>
                {this.props.slides.length === 0 ? (<Skeleton variant='rect' height={360} width='100%' />) :
                    (this.props.slides.map((s, idx) => (
                        <Carousel.Item key={idx}>
                            <img className='img-slide' className="d-block w-100" src={s.imgUrl} alt="slide" />
                            <Carousel.Caption>
                                <h3>{s.Title}</h3>
                                <p>{s.Desc}</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    )))}
            </Carousel>
        );
    }
}