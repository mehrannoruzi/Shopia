import actionTypes from './actionTypes';
import basketSrv from './../../service/basket';

export function AddToBasketAction(product) {
    basketSrv.add(product);
    return {
        type: actionTypes.ADD_TO_BASKET,
        payload: {
            name: product.name,
            maxCount: product.maxCount,
            price: product.price,
            discount: product.discount,
            realPrice: product.realPrice
        }
    };
};

