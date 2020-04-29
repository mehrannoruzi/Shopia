import strings from './../shared/constant';
import addr from './addreses';

export default class addressApi {
    static async getAddresses (token){
        console.log(token);
        try {
            const response = await fetch(`${addr.getAddresses}`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;',
                    'token':token
                }
            });
            const rep = await response.json();
            if (!rep.IsSuccessful)
                return {
                    success: false,
                    message: rep.Message
                }
            else
                return {
                    success: true,
                    result: rep.Result.map((a) => ({
                        id: a.Id,
                        address: a.Address,
                        lat: a.Lat,
                        lng: a.Lng
                    }))
                }
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
        }
    }

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