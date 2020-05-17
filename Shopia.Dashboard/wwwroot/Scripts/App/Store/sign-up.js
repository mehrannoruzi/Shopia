/// <reference path="../../Libs/jquery-3.1.1.min.js" />
$(document).ready(function () {
    $('.btn-check').click(function () {
        console.log('fired');
        let $btn = $(this);
        let $frm = $btn.closest('form');
        let $alert = $frm.find('.alert');
        let btnText = $btn.text();
        $btn.html($threeDotLoader);
        $.get($btn.data('url'), $frm.serialize())
            .done(function (rep) {
                $btn.text(btnText);
                if (rep.IsSuccessful) {
                    $('#modal > .modal-title').text(strings.checkResult);
                    $('#modal > .modal-body-content').html(rep.Result);
                    $('#modal').modal('show');
                }
                else {
                    $alert.find('.text').text(rep.Message);
                    $alert.slideDown();
                    setTimeout(function () {
                        $alert.slideUp();
                    }, 2000);
                }
            })
            .fail(function () {
                $btn.text(btnText);
                $('#modal').modal('hide');
            });
    });
});