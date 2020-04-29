import addr from './addreses';
import strings from './../shared/constant';

export default class storeApi {
    static async getSingleStore(id) {
        try {
            const response = await fetch(`${addr.getSingleStore}?id=${id}`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;'
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
                        name: rep.Result.Name,
                        logoUrl: rep.Result.LogoUrl
                    }
                }
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
        }

    }
}