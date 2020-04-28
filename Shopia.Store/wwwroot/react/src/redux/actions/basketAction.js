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
            imgUrl: product.slides.length > 0 ? product.slides[0].imgUrl : null,
            count,
        }
    };
};

export function UpdateBasketAction(id, count) {
    return {
        type: actionTypes.UPDATE_BASKET,
        payload: { id, count }
    };
};

export function RemoveFromBasketAction(id) {
    return {
        type: actionTypes.REMOVE_FROM_BASKET,
        payload: { id }
    };
};
