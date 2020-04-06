import { getUserInfo } from './../shared/utils';

export default class addressApi {
    static getAllAddress = () => new Promise((resolve) => {
        let userRep = getUserInfo();
        setTimeout(function () {
            resolve({
                success: true,
                result: [{
                    id: 1,
                    lat: 35.699729,
                    lng: 51.337941,
                    details: 'میدان آزادی- کوچه بهار'
                }]
            });
        }, 1000)

    });
    static getSingleAddress = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    id: 1,
                    lat: 35.699729,
                    lng: 51.337941,
                    details: 'میدان آزادی- کوچه بهار'
                }
            });
        }, 1000)

    });
}