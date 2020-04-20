export default class BasketSrv {
    static key = 'basket';
    static add(product) {
        try {
            let jsonBasket = localStorage.getItem(this.key);
            let basket = [];
            if (jsonBasket)
                basket = JSON.parse(jsonBasket);
            basket.push(product);
            jsonBasket = JSON.stringify(basket);
            localStorage.setItem(this.key, jsonBasket);
        }
        catch{
            let basket = [];
            basket.push(product);
            let jsonBasket = JSON.stringify(basket);
            localStorage.setItem(this.key, jsonBasket);
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

    static getCount(){
        let jsonBasket = localStorage.getItem(this.key);
        let basket = [];
        if (jsonBasket)
            basket = JSON.parse(jsonBasket);
        return basket.length;
    }
}