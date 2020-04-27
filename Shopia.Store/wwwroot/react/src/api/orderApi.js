

export default class orderApi {

    static sendCompleteInfo = (info) => new Promise((resolve) => {

        setTimeout(function () {
            resolve({
                success: true,
                result: '123456-789'
            });
        }, 1000)

    });

    static verify = (orderId) => new Promise((resolve) => {

        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    success: true,
                    traceId:'1234567'
                }
            });
        }, 1000);
    });
}