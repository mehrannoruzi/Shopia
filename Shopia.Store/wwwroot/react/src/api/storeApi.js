import addr from './addreses';
export default class storeApi {
    static getSingleStore = (id) => new Promise((resolve) => {
        //simulate api
        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    name: 'شاپیا | Shopia',
                    desc: 'Shopia & Retail',
                    accountText: 'حساب رسمی شاپیا',
                    deliveryText: 'ارسال 0 تا 100 کالا با ما',
                    gatewayText: 'تخصیص درگاه بانکی مستقل به فروشندگان',
                    logoUrl: 'https://www.instagram.com/static/images/ico/apple-touch-icon-120x120-precomposed.png/8a5bd3f267b1.png',
                    followersCount: '3000',
                    postsCount: '500',
                    followingCount: '100',
                }
            });
        }, 1000)

    });
}