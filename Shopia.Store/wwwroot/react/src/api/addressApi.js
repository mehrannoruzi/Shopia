import strings from './../shared/constant';
import addr from './addreses';

export default class addressApi {
    static async getAddresses(token) {
        try {
            const response = await fetch(`${addr.getAddresses}`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;',
                    'token': token
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

    static async getDeliveryCost(token, address) {
        try {
            const response = await fetch(`${addr.getAddresses}?lng=${address.lng}&lat=${address.lat}&address=${address.address}`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;',
                    'token': token
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
                    result: {
                        postCost: rep.Result.PostCost,
                        peykCost: rep.Result.PeykCost
                    }
                }
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
        }
    }

}