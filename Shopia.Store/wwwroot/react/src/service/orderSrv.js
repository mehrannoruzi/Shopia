import CryptoJS from 'crypto-js';
import orderApi from './../api/orderApi';

export default class orderSrv {

    static infoKey = 'order_info';

    static infoExpireInDays = 366;

    static async addInfo(info) {
        let savedInfo = this.getInfo();
        let cdt = new Date();
        cdt.setDate(cdt.getDate() + this.infoExpireInDays);
        let token = null;
        if (savedInfo) {
            token = savedInfo.token;
            localStorage.setItem(this.infoKey, JSON.stringify({ ...savedInfo,...info, expDateTime: cdt.getTime() }));
            if (savedInfo.fullname === info.fullname || savedInfo.mobileNumber === info.mobileNumber || savedInfo.description === info.description)
                return { success: true, result: savedInfo };
        }
        info.token = token;
        let callRep = await orderApi.sendCompleteInfo(info);
        if (!callRep.success)
            return callRep;
        info.token = callRep.result;
        info.expDateTime = cdt.getTime();
        localStorage.setItem(this.infoKey, JSON.stringify(info));
        return { success: true };
    }

    static getInfo() {
        let jsonInfo = localStorage.getItem(this.infoKey);
        if (jsonInfo) {
            let info = JSON.parse(jsonInfo);
            if (info.expDateTime > new Date().getTime())
                return info;
            else localStorage.removeItem(this.infoKey);

        }
        return null;
    }
}