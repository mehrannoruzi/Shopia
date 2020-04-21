export default class productApi {
    static getProducts = (storeId, category, pageNumber, pageSize) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true,
                result: [{
                    id: 1,
                    name: 'اتو مو',
                    likeCount: 20,
                    price: 50000,
                    realPrice: 50000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 0,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg'
                },
                {
                    id: 2,
                    name: 'فر مو',
                    likeCount: 10,
                    price: 55000,
                    realPrice: 42000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 5,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg'
                },
                {
                    id: 3,
                    name: 'مزه مصنوعی',
                    likeCount: 10,
                    price: 55000,
                    realPrice: 55000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 0,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg'
                },
                {
                    id: 4,
                    name: 'تل مو',
                    likeCount: 10,
                    price: 55000,
                    realPrice: 47000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 4,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg'
                },
                {
                    id: 5,
                    name: 'کش مو',
                    likeCount: 10,
                    price: 57000,
                    realPrice: 48000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 8,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg'
                },
                {
                    id: 6,
                    name: 'بند مو',
                    likeCount: 4,
                    price: 55000,
                    realPrice: 49000,
                    currency: 'تومان',
                    maxCount: 3,
                    discount: 4,
                    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg'
                }]
            });
        }, 3000)

    });

    static getSingleProduct = (id) => new Promise((resolve) => {
        try {
            setTimeout(function () {
                resolve({
                    success: true,
                    code: 200,
                    result: {
                        id: 6,
                        name: 'بند مو',
                        likeCount: 4,
                        price: 55000,
                        realPrice: 49000,
                        currency: 'تومان',
                        maxCount: 3,
                        discount: 4,
                        desc:'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با',
                        slides: [{ imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg' },
                        { imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg' }]
                    }
                });
            }, 3000);
        }
        catch{
            resolve({ code: 404 });
        }



    });

    static toggleLike = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true
            });
        }, 3000)

    });

    static toggleFavorite = (id) => new Promise((resolve) => {
        setTimeout(function () {
            resolve({
                success: true
            });
        }, 3000)

    });
}