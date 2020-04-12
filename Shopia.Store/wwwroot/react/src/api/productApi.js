export default class productApi {
    static getProducts = (storeId, pageNumber, pageSize) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true,
                result: [{
                    name: 'اتو مو',
                    likeCount: 20,
                    price: 50000,
                    count:3,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                },
                {
                    name: 'فر مو',
                    likeCount: 10,
                    price: 55000,
                    count:2,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Us/CT/UsCTo9s7QsnleL9E.jpg'
                },
                {
                    name: 'مزه مصنوعی',
                    likeCount: 10,
                    price: 55000,
                    count:1,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                },
                {
                    name: 'تل مو',
                    likeCount: 10,
                    price: 55000,
                    count:4,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                },
                {
                    name: 'کش مو',
                    likeCount: 10,
                    price: 57000,
                    count:6,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                },
                {
                    name: 'بند مو',
                    likeCount: 4,
                    price: 55000,
                    count:7,
                    imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg'
                }]
            });
        }, 3000)

    });

    static getSingleProduct = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true,
                result: {
                    name: 'اتو مو',
                    likeCount: 20,
                    price: 50000,
                    count:10,
                    slides: [{ imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg' },
                    { imgUrl: 'https://storage.torob.com/backend-api/base/images/Us/CT/UsCTo9s7QsnleL9E.jpg' }]
                }
            });
        }, 3000)

    });

}