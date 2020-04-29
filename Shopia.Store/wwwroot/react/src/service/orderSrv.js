import CryptoJS from 'crypto-js';
import orderApi from './../api/orderApi';
import strings from './../shared/constant';
import basketSrv from './basketSrv';

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
            localStorage.setItem(this.infoKey, JSON.stringify({ ...savedInfo, ...info, expDateTime: cdt.getTime() }));
            if (savedInfo.fullname === info.fullname || savedInfo.mobileNumber === info.mobileNumber || savedInfo.description === info.description)
                return { success: true, result: savedInfo };
        }
        info.token = token;
        let callRep = await orderApi.postCompleteInfo(info);
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

    static async submit(address) {
        let info = this.getInfo();
        if (!info)
            return { success: false, message: strings.doPurchaseProcessAgain };
        let order = {};
        order.user = info;
        order.items = basketSrv.get().map((x) => ({ id: x.id, count: x.count }));
        //order.address = address;
        let apiRep = await orderApi.submit(order);
        console.log(apiRep);
        return apiRep;
    }
}