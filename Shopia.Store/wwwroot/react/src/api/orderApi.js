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
            console.log(rep);
            if (!rep.IsSuccessful) return ({ success: false, message: rep.Message });
            else return ({ success: true, result: rep.Result });
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
        }

    }
    static async submit(order) {
        try {
            const response = await fetch(addr.postOrder, {
                method: 'POST', // *GET, POST, PUT, DELETE, etc.
                mode: 'cors', // no-cors, *cors, same-origin
                //cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
                //credentials: 'same-origin', // include, *same-origin, omit
                headers: {
                    'Content-Type': 'application/json; charset=utf-8;'
                },
                //redirect: 'follow', // manual, *follow, error
                //referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
                body: JSON.stringify(order) // body data type must match "Content-Type" header
            });
            const json = await response.json();
            return json;
        } catch (error) {
            return ({ success: false, message: strings.connecttionFailed });
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