﻿/// <reference path="../../../Libs/jquery-3.1.1.min.js" />
$(document).ready(function () {
    $('.btn-check').click(function () {
        if ($('#Username').val()) return;
        console.log('fired');
        let $btn = $(this);
        let $frm = $btn.closest('form');
        let btnText = $btn.text();
        $btn.html($threeDotLoader);
        $.get($btn.data('url'), $frm.serialize())
            .done(function (rep) {
                console.log(rep);
                $btn.text(btnText);
                if (rep.IsSuccessful) {
                    $('#modal  .modal-title').text(strings.checkResult);
                    $('#modal  .modal-body-content').html(rep.Result);
                    $('#modal').modal('show');
                }
                else showAlert(rep.Message);
                
            })
            .fail(function () {
                $btn.text(btnText);
                $('#modal').modal('hide');
            });
    });

    $('.btn-submit').click(function () {
        let $btn = $(this);
        let $frm = $btn.closest('form');
        if (!$frm.valid()) return;
        let btnText = $btn.text();
        $btn.html($circularLoader);
        console.log(JSON.stringify(customSerialize($frm)));
        //postObjectList()
        $.post($frm.attr('action'), customSerialize($frm))
            .done(function (rep) {
                $btn.html(btnText);
                if (rep.IsSuccessful)
                    window.location.href = rep.Result;
                else {
                    showAlert(rep.Message);
                }
            })
            .fail(function (e) {
                $btn.html(btnText);
            });
    });
});

var showAlert = function (msg) {
    let $alert = $('#error-message');
    $alert.find('.text').text(msg);
    $alert.slideDown();
    setTimeout(function () {
        $alert.slideUp();
    }, 2000);
};