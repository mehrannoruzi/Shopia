import CryptoJS from 'crypto-js';
import strings from './../shared/constant';
import React from 'react'

export function getUserInfo() {
    let ciphertext = localStorage.getItem('user');
    if (ciphertext == null)
        return { success: false, message: strings.notAutheticated, status: 401 };
    var bytes = CryptoJS.AES.decrypt(ciphertext, 'kingofday.ir');
    var user = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
    return {
        success: true,
        result: user
    }
}

export function storeUserInfo(user) {
    let userInfo = JSON.stringify(user);
    let encInfo = CryptoJS.AES.encrypt(userInfo, 'kingofday.ir').toString();
    localStorage.setItem('user', encInfo);
}

export function removeUserInfo() {
    localStorage.removeItem('user');
}

export function arrangeInRows(columnCount, items, wrapperClassName) {
    let rows = [];
    for (let i = 0; i < items.length; i += columnCount) {
        rows.push(<div key={i} className='arraned-row'>{items.slice(i, i + columnCount)}</div>)
    }
    return (<div className={wrapperClassName}>
        {rows}
    </div>);

}