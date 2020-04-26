import strings from './../shared/constant';
import React from 'react'
import { toast } from 'react-toastify';

export function arrangeInRows(columnCount, items, wrapperClassName) {
    let rows = [];
    for (let i = 0; i < items.length; i += columnCount) {
        rows.push(<div key={i} className='arraned-row'>{items.slice(i, i + columnCount)}</div>)
    }
    return (<div className={wrapperClassName}>
        {rows}
    </div>);

}
export function checkLocalStorage() {
    if (!localStorage) toast(strings.useModernBrowser, {type: toast.TYPE.INFO});
}

export function commaThousondSeperator(str) { return str.replace(/\B(?=(\d{3})+(?!\d))/g, ","); };

export function getlocalStorageSizes(){
    var total = 0;
    for(var x in localStorage) {
      var amount = (localStorage[x].length * 2) / 1024 / 1024;
      total += amount;
      console.log( x + " = " + amount.toFixed(2) + " MB");
    }
    console.log( "Total: " + total.toFixed(2) + " MB");
}
