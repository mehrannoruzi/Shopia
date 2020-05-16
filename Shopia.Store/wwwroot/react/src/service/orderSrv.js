import CryptoJS from 'crypto-js';
import orderApi from './../api/orderApi';
import strings from './../shared/constant';
import basketSrv from './basketSrv';

export default class orderSrv {

    static infoKey = 'user_info';
    static orderIdKey = 'order_id';
    static infoExpireInDays = 366;

    static async addInfo(info) {
        let savedInfo = this.getInfo();
        let cdt = new Date();
        cdt.setDate(cdt.getDate() + this.infoExpireInDays);
        let token = null;
        console.log(info);
        if (savedInfo) {
            console.log(savedInfo);
            token = savedInfo.token;
            if (savedInfo.fullname === info.fullname && savedInfo.mobileNumber === info.mobileNumber && savedInfo.description === info.description)
                return { success: true, result: savedInfo };
        }
        info.token = token;
        let callRep = await orderApi.postCompleteInfo(info);
        if (!callRep.success)
            return callRep;
        localStorage.setItem(this.infoKey, JSON.stringify({ ...savedInfo, ...info, expDateTime: cdt.getTime() }));
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

    static async submit(address, reciever, recieverMobileNumber, deliveryId) {
        let info = this.getInfo();
        if (!info)
            return { success: false, message: strings.doPurchaseProcessAgain };
        let order = {};
        order.UserToken = info.token;
        order.description = info.description;
        order.orderId = this.getOrderId();
        order.deliveryId = parseInt(deliveryId);
        order.items = basketSrv.get().map((x) => ({ id: x.id, price: x.price, discount: x.discount, count: x.count }));
        order.address = address;
        order.reciever = reciever;
        order.recieverMobileNumber = recieverMobileNumber;
        let apiRep = await orderApi.submit(order);
        if (apiRep.success) {
            this.setOrderId(apiRep.result.id);
            if (apiRep.result.basketChanged)
                basketSrv.update(apiRep.result.changedProducts);
        }
        return { ...apiRep };
    }
    static getOrderId() {
        let id = localStorage.getItem(this.orderIdKey);
        if (id) return parseInt(id);
        else return null;
    }
    static setOrderId(id) {
        localStorage.setItem(this.orderIdKey, id);
    }
    static clearOrderId() {
        localStorage.removeItem(this.orderIdKey);
    }
}