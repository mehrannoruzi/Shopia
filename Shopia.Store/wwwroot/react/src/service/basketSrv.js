import strings from './../shared/constant';

export default class BasketSrv {
    static key = 'basket';
    static add(product, count) {
        try {
            let jsonBasket = localStorage.getItem(this.key);
            let basket = [];
            if (jsonBasket)
                basket = JSON.parse(jsonBasket);
            let idx = basket.findIndex(x => x.id === product.id);
            if (idx !== -1) return { success: true, result: basket };
            basket.push({ ...product, count });
            jsonBasket = JSON.stringify(basket);
            localStorage.setItem(this.key, jsonBasket);
            return { success: true, result: basket };
        }
        catch{
            let basket = [];
            basket.push(product);
            let jsonBasket = JSON.stringify(basket);
            localStorage.setItem(this.key, jsonBasket);
            return { success: true, result: basket };
        }

    }

    static remove(id) {
        try {
            let jsonBasket = localStorage.getItem(this.key);
            let basket = [];
            if (!basket) return;
            basket = JSON.parse(jsonBasket);
            basket.splice(basket.findIndex(x => x.id = id), 1);
        }
        catch{
        }
    }

    static get() {
        let jsonBasket = localStorage.getItem(this.key);
        let basket = [];
        if (jsonBasket)
            basket = JSON.parse(jsonBasket);
        return basket;
    }

    static getCount() {
        let jsonBasket = localStorage.getItem(this.key);
        let basket = [];
        if (jsonBasket)
            basket = JSON.parse(jsonBasket);
        return basket.length;
    }
}