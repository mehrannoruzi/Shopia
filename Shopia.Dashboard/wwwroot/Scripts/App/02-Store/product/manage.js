/// <reference path="../../../Libs/jquery-3.1.1.min.js" />
var postsPageNumber = 1;
var posts = [];
var selectedPosts = [];
$(document).ready(function () {
    $('.product-manage-page').on('click', '#btn-get-posts', function () {
        getPosts($(this));
    });
    $('.product-manage-page').on('click', '#btn-next-posts', function () {
        if (postsPageNumber === 1) return;
        postsPageNumber--;
        getPosts($(this));
    });
    $('.product-manage-page').on('click', '#btn-prev-posts', function () {
        postsPageNumber++;
        getPosts($(this));
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
            let post = posts.find(function (x) {
                return x.UniqueId === id;
            });
            post.Price = price;
            selectedPosts.push(post);
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
                    $('button.search').trigger('click');
                }
                else {
                    showNotif(notifyType.danger, rep.Message);

                }
            })
            .fail(function (e) {
                ajaxBtn.normal();

            });
    });

    $(document).on('click', '.btn-submit-product', function (e) {
        e.stopPropagation();
        let $btn = $(this);
        submitAjaxForm($btn,
            function (rep) {
                if ($('#ProductId').val() === '0') {
                    $btn.closest('form')[0].reset();
                }
                else {
                    $('#modal').modal('hide');
                }
            },
            null,
            false
        );
    });
});
function getPosts($btn) {
    ajaxBtn.inProgress($btn);
    console.log($btn.data('url'));
    $.get($btn.data('url'), { username: $('#select-store option:selected').data('un'), pageNumber: postsPageNumber })
        .done(function (rep) {
            ajaxBtn.normal();
            if (rep.IsSuccessful) {
                posts = rep.Result.Posts;
                $('#posts-wrapper').html(rep.Result.Partial);
            }
            else {
                showNotif(notifyType.danger, rep.Message);
            }
        })
        .fail(function (e) {
            ajaxBtn.normal();
        });
}

