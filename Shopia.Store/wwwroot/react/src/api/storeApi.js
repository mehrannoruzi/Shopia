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
                    products: [
                        {
                            id:1,
                            name: 'محصول 1',
                            imgUrl: 'https://shopia.me/store/7622291675/2279249235306330715_thumb.jpg'
                        },
                        {
                            id:2,
                            name: 'محصول 2',
                            imgUrl: 'https://shopia.me/store/7622291675/2276375242555414083_thumb.jpg'
                        },
                        {
                            id:3,
                            name: 'محصول 3',
                            imgUrl: 'https://shopia.me/store/7622291675/2274902529592740182_thumb.jpg'
                        },
                        {
                            id:4,
                            name: 'محصول 4',
                            imgUrl: 'https://shopia.me/store/7622291675/2274136810626104389_thumb.jpg'
                        }
                    ]
                }
            });
        }, 1000)

    });
}