

export default class orderApi {
    static getInfo = (id) => new Promise((resolve) => {
        
        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    storeId:1,
                    productCount: 2,
                    productCode: 'p-12',
                    traceId: '123',
                    deliverDateTime: 'یکشنبه ار 09 تا 12',
                    cost: 55000,

                }
            });
        }, 1000)

    });

}