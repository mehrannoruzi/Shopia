import addr from './addreses';
export default class storeApi {
    static getSingleStore = (id) => new Promise((resolve) => {
        //simulate api
        setTimeout(function () {
            resolve({
                success: false,
                result: {
                    name: 'شاپیا | Shopia',
                    logoUrl: 'https://www.instagram.com/static/images/ico/apple-touch-icon-120x120-precomposed.png/8a5bd3f267b1.png'
                }
            });
        }, 1000)

    });
}