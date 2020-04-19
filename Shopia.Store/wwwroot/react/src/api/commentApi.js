import { getUserInfo } from './../shared/utils';

export default class addressApi {
    static getComments = (productId) => new Promise((resolve) => {
        let userRep = getUserInfo();
        setTimeout(function () {
            resolve({
                success: true,
                result: [{
                    id:1,
                    text: 'کامنت 1'
                },
                {
                    id:2,
                    text: 'کامنت 2'
                }]
            });
        }, 1000)

    });
    static addComment = (producrId, text) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true,
                result: 1
            });
        }, 1000)

    });

    static deleteComment = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true
            });
        }, 1000)

    });

}