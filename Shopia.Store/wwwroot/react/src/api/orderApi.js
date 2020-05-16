import addr from './addreses';
import strings from './../shared/constant';

export default class orderApi {

    static async postCompleteInfo(info) {
        try {
            const response = await fetch(addr.postCompleteInfo, {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;'
                },
                body: JSON.stringify(info)
            });
            const rep = await response.json();
            
            if (!rep.IsSuccessful) return ({ success: false, message: rep.Message });
            else return ({ success: true, result: rep.Result });
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
        }

    }
    static async submit(order) {
        try {
            const response = await fetch(addr.postOrder, {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;'
                },
                body: JSON.stringify(order) // body data type must match "Content-Type" header
            });
            const rep = await response.json();
            if (rep.IsSuccessful)
                return {
                    success: true,
                    result: {
                        id: rep.Result.OrderId,
                        basketChanged: rep.Result.BasketChanged,
                        url:rep.Result.Url,
                        changedProducts: rep.Result.BasketChanged ? rep.Result.ChangedProducts.map((p) => ({
                            id: p.Id,
                            price: p.Price,
                            discount: p.Discount,
                            count:p.Count
                        })) : []
                    }
                };
            else return { success: false, message: rep.Message };
        }
        catch (error) {
            console.log(error);
            return { success: false, message: strings.connecttionFailed };
        }
    }

    static verify = (orderId) => new Promise((resolve) => {

        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    success: true,
                    traceId: '1234567'
                }
            });
        }, 1000);
    });
}