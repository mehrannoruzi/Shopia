import strings from './../shared/constant';

export default class BasketSrv {
    static key = 'basket';
    static add(product, count) {
        let imgUrl = product.slides.length > 0 ? product.slides[0].imgUrl : null;
        let p = { ...product, imgUrl, count };
        delete p.slides;

        try {
            let basket = this.get();
            let idx = basket.findIndex(x => x.id === product.id);
            if (idx !== -1) basket[idx].count = count;
            else basket.push(p);
            localStorage.setItem(this.key, JSON.stringify(basket));
            return { success: true, result: basket };
        }
        catch{
            let basket = [];
            basket.push(p);
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

    static update(products) {
        let basket = this.get();
        products.forEach(p => {
            let idx = basket.findIndex(x => x.id === p.id);
            if (idx > -1) {
                if (p.maxCount === 0) basket.splice(idx, 1);
                else {
                    basket[idx].price = p.price;
                    basket[idx].discount = p.discount;
                    basket[idx].realPrice = p.realPrice;
                }
            }

        });
        localStorage.setItem(this.key, JSON.stringify(basket));
    }

    static clear() {
        localStorage.removeItem(this.key);
    }
}