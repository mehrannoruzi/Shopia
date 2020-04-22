import actionTypes from './actionTypes';

export function AddToBasketAction(product, count) {
    return {
        type: actionTypes.ADD_TO_BASKET,
        payload: {
            id: product.id,
            name: product.name,
            maxCount: product.maxCount,
            price: product.price,
            discount: product.discount,
            realPrice: product.realPrice,
            count
        }
    };
};

export function RemoveFromBasketAction(id) {
    return {
        type: actionTypes.REMOVE_FROM_BASKET,
        payload: {id}
    };
};
