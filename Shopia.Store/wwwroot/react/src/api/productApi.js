import strings from './../shared/constant';

const products = [{
    id: 1,
    name: 'اتو مو',
    likeCount: 20,
    price: 50000,
    realPrice: 50000,
    currency: 'تومان',
    maxCount: 3,
    discount: 0,
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
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
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
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
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
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
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
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
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
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
    desc: 'لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع ',
    imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg'
}];
export default class productApi {
    static getProducts = (storeId, category, pageNumber, pageSize) => new Promise((resolve) => {
        try {
            setTimeout(function () {
                resolve({
                    success: true,
                    result: products
                });
            }, 3000)
        }
        catch{
            resolve({ success: false, message: strings.connecttionFailed });
        }


    });

    static getSingleProduct = (id) => new Promise((resolve) => {
        try {
            let p = products.find(x => x.id === parseInt(id));
            setTimeout(function () {
                resolve({
                    success: true,
                    result: {
                        ...p,
                        slides: [{ imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg' },
                        { imgUrl: 'https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg' }]
                    }
                });
            }, 3000);
        }
        catch{
            resolve({ success: false, message: strings.connecttionFailed });
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