/// <reference path="../../../Libs/jquery-3.1.1.min.js" />
var postsPageNumber = 1;
var posts = [];
var selectedPosts = [];
$(document).ready(function () {
    $('.product-manage-page').on('click', '#btn-get-posts', function () {
        let $btn = $(this);
        console.log($('#select-store option:selected').data('un'));
        ajaxBtn.inProgress($btn);
        $.get($btn.data('url'), { username: $('#select-store option:selected').data('un'), pageNumber: postsPageNumber})
            .done(function (rep) {
                ajaxBtn.normal();
                console.log(rep);
                if (rep.IsSuccessful) {
                    posts = rep.Result.Posts;
                    $('#posts-wrapper').html(rep.Result.Partial);
                }
                else {

                }
            })
            .fail(function (e) {
                ajaxBtn.normal();
            });
    });

    $('.product-manage-page').on('input', '.input-price', function () {
        let $i = $(this);
        let $figure = $i.closest('figure');
        let num = $i.val();
        if (isNaN(num)) {
            $i.addClass('color-red');
            $figure.removeClass('valid');
            return;
        }
        else $i.removeClass('color-red');
        let price = parseInt(num);
        if (price > 1000) {
            $figure.addClass('valid');
            let id = $i.data('id');
            selectedPosts.push(posts.find(function (x) {
                return x.UniqueId === id;
            }));
        }
        else {
            $figure.removeClass('valid');
        }
    });

    $('.product-manage-page').on('click', '#btn-submit', function () {
        let $btn = $(this);
        if (selectedPosts.length === 0) {
            showNotif(notifyType.warning, strings.pleasePriceAtleastOne);
            return;
        }
        ajaxBtn.inProgress($btn);
        $.post($btn.data('url'), { storeId: $('#select-store').val(), posts: selectedPosts })
            .done(function (rep) {
                console.log(rep);
                ajaxBtn.normal();
                if (rep.IsSuccessful) {
                    showNotif(notifyType.success, rep.Message);
                }
                else {
                    showNotif(notifyType.danger, rep.Message);

                }
            })
            .fail(function (e) {
                ajaxBtn.normal();

            });
    });
});

