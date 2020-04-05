export default class productApi {
    static getProduct = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                name: 'اتو مو',
                likeCount: 20,
                price: 50000,
                slides: [
                    { imgUrl: 'https://storage.torob.com/backend-api/base/images/Ta/hO/TahOLrRj5RRh9W9n.jpg' },
                    { imgUrl: 'https://storage.torob.com/backend-api/base/images/Us/CT/UsCTo9s7QsnleL9E.jpg' }]
            });
        }, 3000)

    });
}