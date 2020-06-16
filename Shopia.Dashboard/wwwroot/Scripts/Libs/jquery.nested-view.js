(function ($) {

	$.fn.nestedView = function (options) {
		let $elm = $(this);
		let fired = $elm.data('fired');
		if (fired === 'true') return;
		$elm.data('fired', 'true');
		options = typeof options !== 'undefined' ? options : {};
		let $btns = '<div class="box">';
		let addable = typeof options.addFormHtml !== 'undefined' ? true : false;
		$btns += '<input class="form-control" id="input-search" type="text"/>';
		if (addable)
			$btns += '<button type="button" class="btn btn-add btn-primary">' + $elm.data('add-text') + '</button>';
		let editable = typeof $elm.data('edit-url') !== 'undefined' ? true : false;
		if (editable)
			$btns += '<button type="button" class="btn btn-edit btn-info">' + $elm.data('edit-text') + '</button>';
		let deletable = typeof $elm.data('delete-url') !== 'undefined' ? true : false;
		if (deletable)
			$btns += '<button type="button" class="btn btn-delete btn-danger">' + $elm.data('delete-text') + '</button>';
		$btns += '</div>';
		//===================================================================
		//-- Commands
		//===================================================================
		let openItem = function ($item) {
			let $li = $item.parent();
			if ($li.hasClass('open')) return;
			let $subMenu = $item.next();
			let $wrapperLi = $li.parent().closest('li');
			$elm.find('li').not($wrapperLi).removeClass('open');
			$li.addClass('open');
			$subMenu.slideDown(500);
			$elm.find('.box').remove();
			if ($item.find('.box').length === 0)
				$item.append($btns);
		};
		let closeItem = function ($item) {
			let $li = $item.parent();
			if (!$li.hasClass('open')) return;
			let $subMenu = $item.next();
			$li.removeClass('open');
			$subMenu.slideUp(500);
			$item.find('.box').remove();
			$li.find('form').remove();
		};
		let openAll = function () {
			$elm.find('.item').each(function () { openItem($(this)); });
		};
		let closeAll = function () {
			$elm.find('.item').each(function () { closeItem($(this)); });
		};
		//===================================================================
		//-- events
		//===================================================================
		//
		$(document).on('input', '.nested-view #input-search', function (e) {
			closeAll();
			let txt = $(this).val();
			let $items = [];
			$elm.find('.item').each(function () {
				if ($(this).find('span.name').text().indexOf(txt) > -1) $items.push($(this));
			});

		});
		//add to root
		$(document).on('click', '.nested-view #btn-add-root', function (e) {
			e.stopPropagation();
			let $btn = $(this);
			let $root = $btn.parent();
			var html = options.addFormHtml.replace('##ParentId##', '');
			$root.append(html);
			$.validator.unobtrusive.parse($root);
		});
		//show all
		$(document).on('click', '.nested-view #btn-show-all', function (e) {
			e.stopPropagation();
			let $btn = $(this);
			let openMode = $btn.data('open');
			if (openMode === 'true') closeAll();
			else openAll();
			//$(this).closest('.nested-view').find('.item').each(function () {
			//	let $item = $(this);
				//let $li = $item.parent();
				//let $menu = $item.next();
				//if (openMode === 'true') {
				//	closeItem($item);
					//$btn.data('open', 'false');
					//$li.removeClass('open');
					//$menu.slideUp(500);
					//$item.find('.box').remove();
					//$li.find('form').remove();
				//}
				//else {
				//	openItem($item);
					//$btn.data('open', 'true');
					//$li.addClass('open');
					//$menu.slideDown(500);
					//if ($item.find('.box').length === 0)
					//	$item.append($btns);
				//}
			//});
		});
		//show item
		$(document).on('click', '.nested-view .item', function (e) {
			e.stopPropagation();
			let $item = $(this);
			let $li = $item.parent();
			let $menu = $item.next();
			let open = $li.hasClass('open');
			if (open) {
				closeItem($item);
				//$li.removeClass('open');
				//$menu.slideUp(500);
				//$item.find('.box').remove();
				//$li.find('form').remove();
			}
			else {
				openItem($item);
				//let $wrapperLi = $li.parent().closest('li');
				//$elm.find('li').not($wrapperLi).removeClass('open');
				//$li.addClass('open');
				//$menu.slideDown(500);
				//$elm.find('.box').remove();
				//if ($item.find('.box').length === 0)
				//	$item.append($btns);
			}
		});
		//show add item form
		$(document).on('click', '.nested-view .btn-add', function (e) {
			e.stopPropagation();
			console.log('add');
			let $btn = $(this);
			let id = $btn.closest('.item').data('id');
			let $li = $btn.closest('li');
			var html = options.addFormHtml.replace('##ParentId##', id);
			$li.append(html);
			$.validator.unobtrusive.parse($li);
		});
		//show edit form
		$(document).on('click', '.nested-view .btn-edit', function (e) {
			e.stopPropagation();
			let $btn = $(this);
			let id = $btn.closest('.item').data('id');
			let $li = $btn.closest('li');
			ajaxBtn.inProgress($btn);
			$.get($elm.data('edit-url'), { id: id })
				.done(function (rep) {
					console.log(rep);
					ajaxBtn.normal();
					if (rep.IsSuccessful) {
						$li.append(rep.Result);
						$.validator.unobtrusive.parse($li);
					}
					else {
						showNotif(notifyType.danger, rep.Message);
					}
				})
				.fail(function (rep) {
					ajaxBtn.normal();
				});

		});
		//show edit form
		$(document).on('click', '.nested-view .btn-delete', function (e) {
			e.stopPropagation();
			let $btn = $(this);
			let id = $btn.closest('.item').data('id');
			let $li = $btn.closest('li');
			ajaxBtn.inProgress($btn);
			$.post($elm.data('delete-url'), { id: id })
				.done(function (rep) {
					if (rep.IsSuccessful) {
						$li.remove();
					}
					else {
						showNotif(notifyType.danger, rep.Message);
					}
					ajaxBtn.normal();
				})
				.fail(function (rep) {
					ajaxBtn.normal();
				});

		});
		return this;
	};
})(jQuery);