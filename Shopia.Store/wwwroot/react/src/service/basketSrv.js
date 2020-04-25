import strings from './../shared/constant';

export default class BasketSrv {
    static key = 'basket';
    static add(product, count) {
        try {
            let basket = this.basket();
            let idx = basket.findIndex(x => x.id === product.id);
            if (idx !== -1) basket[idx].count = count;
            else basket.push({ ...product, count });
            localStorage.setItem(this.key, JSON.stringify(basket));
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
            let basket = this.get();
            if (basket.legnth == 0) return;
            basket.splice(basket.findIndex(x => x.id === id), 1);
            localStorage.setItem(this.key, JSON.stringify(basket));
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

    static updateCount(id, count) {
        try {
            let basket = this.get();
            let item = basket.find(x => x.id == id);
            item.count = count;
            localStorage.setItem(this.key, JSON.stringify(basket));
            return { success: true }
        }
        catch{
            return {
                success: false,
                message: strings.retryPlease
            }
        }

    }
}