import strings from './../shared/constant';
import addr from './addreses';

export default class addressApi {
    static getAddresses = (token) => new Promise((resolve) => {
        //let userRep = getUserInfo();
        setTimeout(function () {
            resolve({
                success: true,
                result:
                    [{
                        id: 1,
                        lat: 35.699729,
                        lng: 51.337941,
                        address: 'میدان آزادی- کوچه بهار'
                    },
                    {
                        id: 2,
                        lat: 35.90,
                        lng: 51.337941,
                        address: 'میدان انقلاب- کوچه نسیم'
                    }]
            });
        }, 1000)

    });
    static getDeliveryCost = (token, address) => new Promise((resolve) => {
        try {
            setTimeout(function () {
                resolve({
                    success: true,
                    result: {
                        id:4,
                        cost: 15000,
                        currency:'تومان'
                    }
                });
            }, 1000)
        }
        catch{
            resolve({
                success: false,
                message: strings.connecttionFailed
            });
        }

    });

}