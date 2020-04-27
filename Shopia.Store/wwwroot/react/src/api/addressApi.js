

export default class addressApi {
    static getAddresses = (token) => new Promise((resolve) => {
        //let userRep = getUserInfo();
        setTimeout(function () {
            resolve({
                success: true,
                result: [{
                    id: 1,
                    lat: 35.699729,
                    lng: 51.337941,
                    address: 'میدان آزادی- کوچه بهار'
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
                    reciever: 'مهران نوروزی',
                    lng: 51.337941,
                    lat: 34.699729,
                    address: 'میدان آزادی- کوچه بهار'
                }
            });
        }, 1000)

    });

    static addAddress = ({ address, reciever, lng, lat }) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true
            });
        }, 1000)

    });

    static updateAddress = ({ id, address, reciever, lng, lat }) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true
            });
        }, 1000)

    });

}