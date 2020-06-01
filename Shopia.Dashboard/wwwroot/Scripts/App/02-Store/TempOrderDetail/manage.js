/// <reference path="../../../Libs/jquery-3.1.1.min.js" />
var items = [];
var products = [];

$(document).ready(function () {
    //====================================================================== add product initital info to inputs
    //======================================================================
    $('#modal').on('select2:select', '#ProductId', function () {
        console.log('fired');
        let id = $(this).val();
        if (products) {
            let product = products.find(function (p) { return p.id === parseInt(id); });
            if (product) {
                $('#Count').val('1');
                $('#Price').val(product.price);
                $('#TotalPrice').val(product.price);
            }
        }
    });
    //====================================================================== Add Item
    //======================================================================

    $('#modal').on('click', '#btn-add-item', function (e) {
        e.stopPropagation();
        let $frm = $(this).closest('form');
        if (!$frm.valid()) return;
        let id = $('#ProductId').val();
        console.log(id);
        console.log(products);
        let p = products.find(function (x) {
            return x.id === parseInt(id);
        });
        console.log(p);
        if (!p) return;
        items.push({
            productId: parseInt(id),
            count: parseInt($('#Count').val()),
            name: p.name,
            price: p.price,
            totalPrice: parseInt($('#TotalPrice').val())
        });
        createRows();
        products = [];
        $('#ProductId').select2("val", "");
        $('#frm-item')[0].reset();
    });
    //====================================================================== remove
    //======================================================================

    $('#modal').on('click', '.close', function (e) {
        e.stopPropagation();
        let id = $(this).data('id');
        $(this).closest('tr').remove();
        let idx = items.findIndex(function (p) {
            return p.productId === id;
        });
        items.splice(idx, 1);
    });
    //====================================================================== Submit
    //======================================================================

    $('#modal').on('click', '.btn-submit-items', function (e) {
        e.stopPropagation();
        let $btn = $(this);
        ajaxBtn.inProgress($btn);
        $.ajax({
            type: 'POST',
            url: $btn.data('url'),
            data: JSON.stringify(items),
            contentType: 'application/json; charset=utf-8;',
            success: function (rep) {
                console.log(rep);
                ajaxBtn.normal();
                if (!rep.IsSuccessful)
                    showNotif(notifyType.danger, rep.Message);
                else {
                    $('#add-wrapper').html(rep.Result);
                    $('.btn-submit-items').hide();
                    // $('#modal').modal('hide');
                }

            },
            error: function (e) {
                ajaxBtn.normal();

            }
        });
    });
});
var createRow = function (p, idx) {
    return '<tr data-id="' + p.productId + '">' +
        '<td>' + idx + '</td>' +
        '<td>' + p.name + '</td>' +
        '<td>' + p.count + '</td>' +
        '<td>' + commaThousondSeperator(p.price.toString()) + '</td>' +
        '<td>' + commaThousondSeperator(p.totalPrice.toString()) + '</td>' +
        '<td><i class="close zmdi zmdi-close default-i"></i></td>' +
        '</tr>';
};
var createRows = function () {
    if (items.length > 0) $('#items-wrapper').removeClass('d-none');
    let $target = $('#items').empty();
    for (var i = 0; i < items.length; i++)
        $target.append(createRow(items[i], i + 1));

};