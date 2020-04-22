import strings from './../shared/constant';

export default class generalApi {
    static getContactUsInfo = () => new Promise((resolve) => {
        try {
            setTimeout(function () {
                resolve({
                    success: true,
                    result: {
                        whatsappLink: 'https://wa.me/989116107197',
                        telegramLink: 'https://t.me/kingofday',
                        phoneNumbers: ['9334188188', '933561109'],
                        websiteName: 'shopia.ir',
                        websiteUrl: 'https://kingofday.ir'
                    }
                });
            }, 1000);
        }
        catch{
            resolve({ success: false, message: strings.connecttionFailed });
        }


    });

}