import React from 'react';
import { connect } from 'react-redux';
import strings from '../../shared/constant';
import orderApi from './../../api/orderApi';
import greenBasketImage from './../../assets/images/green-basket.svg';
import redBasketImage from './../../assets/images/red-basket.svg';
import Skeleton from '@material-ui/lab/Skeleton';
import { ShowInitErrorAction, HideInitErrorAction } from "../../redux/actions/InitErrorAction";
import Header from './../../shared/header';
import Steps from './../../shared/steps';

class AfterGateway extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            success: null,
            traceId: null
        }
    }

    async _fetchData() {
        const { params } = this.props.match;
        let apiRep = await orderApi.verify(params.orderId);
        if (!apiRep.success) {
            this.props.showInitError(this._fetchData.bind(this), apiRep.message);
            return;
        }
        this.setState(p => ({
            ...p,
            loading: false,
            success: apiRep.result.success,
            traceId: apiRep.result.traceId
        }));
    }

    async componentDidMount() {
        this.props.hideInitError();
        await this._fetchData();
    }

    _goBack() {

    }

    render() {
        return (
            <div className="after-gateway-page">
                <Header hasTitle={true} goBack={this.props.history.goBack} />
                <Steps activeStep={2} />
                <div className='content'>
                    {this.state.loading ? <Skeleton variant='circle' height={107} width={107} /> :
                        <img className='img-basket' src={this.state.success ? greenBasketImage : redBasketImage} alt='basket' />}

                    {this.state.loading ? <Skeleton className='main-message' width={120} height={25} variant='text' /> :
                        <span className={'main-message ' + (this.state.success ? 'success' : 'error')}>
                            {this.state.success ? strings.thankYouForPurchase : strings.purchaseFailed}
                        </span>}
                    {this.state.loading ? <Skeleton  className='hint' width={120} variant='text' /> :
                        <span className='hint'>
                            {this.state.success ? strings.successfulOrder : strings.retryPlease}
                        </span>}

                    {this.state.loading ? <Skeleton  className='trace-id-text' width={120} variant='text' /> :
                        <span className='trace-id-text'>
                            {strings.orderTraceIdIs}
                        </span>}

                    {this.state.loading ? <Skeleton  className='m-b trace-id' width={50} variant='text' /> :
                        <span className='m-b trace-id'>
                            {this.state.traceId}
                        </span>}
                    {!this.state.loading && this.state.success ?
                        <span>
                            {strings.callYouLater}
                        </span> : <Skeleton width={120} variant='text' />}
                </div>

            </div>
        );
    }
}

const mapDispatchToProps = dispatch => ({
    showInitError: (fetchData, message) => dispatch(ShowInitErrorAction(fetchData, message)),
    hideInitError: () => dispatch(HideInitErrorAction())
});
export default connect(null, mapDispatchToProps)(AfterGateway);