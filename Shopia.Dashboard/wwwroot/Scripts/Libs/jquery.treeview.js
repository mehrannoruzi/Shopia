(function ($) {

	$.fn.tRex = function (option) {
		//Options
		let defaults = $.extend({}, {
			loadUrl: '',
			autoExpand: true,
			formUrl: '',
			addUrl: '',
			editUrl: '',
			deleteUrl: '',
			dic: {
				add: 'افزودن',
				edit: 'ویرایش',
				delete: 'حذف',
				addSubItem: 'افزودن زیر دسته',
				searchBoxPlaceHolder: 'جستجو...',
				showAll: 'باز کردن همه',
				dontShowAll: 'بستن همه',
				btnSubmit: 'ثبت',
				btnCancel: 'لغو'
			},
			accessibility: {
				showAddParent: true,
				showAddSubItem: true,
				showEditItem: true,
				showDeleteItem: true
			},
			deleteSelectedItem: function ($callback) { },
			submitForm: function ($callback) { },
			cancelForm: function ($callback) { }
		}, option);

		//Wrapper selector
		let $this = $(this);

		let $jsonData = {};

		//Initialize Plugin
		var _init = function () {
			loadDataFromUrl(function ($json) {
				if ($.isEmptyObject($json)) {
					ifEmptyData();
				} else {
					loadRoot();
					expandOrCollapseTree(defaults.autoExpand);
				}
			}, function ($msg) {
				console.log($msg);
			});
		};

		//Get all data from url (With ajax)
		var loadDataFromUrl = function ($callbackSuccess, $callbackError) {
			let message = '';

			if (defaults.loadUrl === null || defaults.loadUrl === '') {
				message = 'Invalid LoadUrl!';
				$callbackError(message);
			} else {
				$.get(defaults.loadUrl, {})
					.done(function (rep) {
						if (rep.IsSuccessful === true) {
							if (!validator.isJson(rep.Result)) {
								message = 'Invalid Data JSON type!';
								$callbackError(message);
							}
							else {
								$jsonData = typeof rep.Result === 'string'
									? JSON.parse(rep.Result)
									: rep.Result;

								$callbackSuccess($jsonData);
							}
						} else {
							$callbackSuccess($jsonData);
						}
					})
					.fail(function (e) {
						message = 'Error in get data from "' + defaults.loadUrl + '"';
						$callbackError(message);
					});
			}
		};

		//Expand or Collapse treeview
		var expandOrCollapseTree = function (state) {
			let allParent = $('.item-root.item-has-child');
			let expColpsBtn = $('.item-appender .show-all');
			if (allParent.length > 0) {

				if (defaults.autoExpand === true) {
					expColpsBtn.attr('class', '').addClass('show-all expand').text(defaults.dic.dontShowAll);
				} else {
					expColpsBtn.attr('class', '').addClass('show-all collaps').text(defaults.dic.showAll);
				}

				$.each(allParent, function (idx, val) {
					let pin = $(this).find('> i.zmdi');
					let subItems = $(this).closest('.item-wrapper').find('> .item-sub-items');
					if (state === true) {
						pin.attr('class', '').addClass('zmdi zmdi-minus-square');
						subItems.show();
					} else {
						pin.attr('class', '').addClass('zmdi zmdi-plus-square');
						subItems.hide();
					}
				});
			} else {
				expColpsBtn.attr('class', '').addClass('show-all hide').text(defaults.dic.showAll);
			}
		};

		//Validator functions
		var validator = {
			isJson: function (item) {
				item = typeof item !== 'string'
					? JSON.stringify(item)
					: item;

				try {
					item = JSON.parse(item);
				} catch (e) {
					return false;
				}

				if (typeof item === 'object' && item !== null) return true;
				return false;
			}
		};

		//show result if data is empaty!
		var ifEmptyData = function () {
			let template = '<div class="item-wrapper">' +
				'				<div class="item-root item-appender">' +
				'					<button type="button" class="add-item ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-id="0" data-item-pid="0">' + defaults.dic.add + '</button>' +
				'				</div>' +
				'			<div class="item-edit-data"></div>' +
				'			</div>';

			$this.html(template);
		};

		//generate root items and continue to generate cascade sub items
		var loadRoot = function () {
			let template = '';
			let filteredItems = filterItems(0);
			if (filteredItems.length > 0) {

				template += '<div id="search-wrapper">' +
					'			<input type="text" class="search-items" placeholder="' + defaults.dic.searchBoxPlaceHolder + '"/>' +
					'			<i class="zmdi zmdi-close clear-text"></i>' +
					'			<i class="zmdi zmdi-search icon-search"></i>' +
					'		 </div>';

				$.each(filteredItems, function (idx, val) {
					let subLevels = filterItems(val.ItemId);
					if (subLevels.length > 0) {
						let subTemplate = loadSubLevel(subLevels, val.ItemId);
						template += '<div class="item-wrapper is-parent">' +
							'			<div class="item-root item-has-child">' +
							'				<i class="zmdi zmdi-plus-square"></i>' +
							'				<span class="item-title">' + val.Name + '</span>' +
							'				<div class="item-action">' +
							'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + val.ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
							'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="0" data-item-id="' + val.ItemId + '">' + defaults.dic.edit + '</button>' +
							'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="0" data-item-id="' + val.ItemId + '">' + defaults.dic.delete + '</button>' +
							'				</div>' +
							'			</div>' +
							'			<div class="item-edit-data"></div>' +
							'			<div class="item-sub-items">' +
							subTemplate +
							'			</div>' +
							'		 </div>';

					} else {
						template += '<div class="item-wrapper">' +
							'			<div class="item-root item-no-child">' +
							'				<i class="zmdi zmdi-check-circle"></i>' +
							'				<span class="item-title">' + val.Name + '</span>' +
							'				<div class="item-action">' +
							'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + val.ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
							'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="0" data-item-id="' + val.ItemId + '">' + defaults.dic.edit + '</button>' +
							'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="0" data-item-id="' + val.ItemId + '">' + defaults.dic.delete + '</button>' +
							'				</div>' +
							'			</div>' +
							'			<div class="item-edit-data">' +
							'			</div>' +
							'		 </div>';
					}
				});

				template += '<div class="item-wrapper">' +
					'			<div class="item-root item-appender">' +
					'				<button type="button" class="add-item ' + (defaults.accessibility.showAddParent === true ? '' : 'hide') + '" data-item-id="0" data-item-pid="0">' + defaults.dic.add + '</button>' +
					'				<button type="button" class="show-all ' + (defaults.autoExpand === true ? 'expand' : 'collaps hide') + '">' + (defaults.autoExpand === true ? defaults.dic.dontShowAll : defaults.dic.showAll) + '</button>' +
					'			</div>' +
					'			<div class="item-edit-data"></div>' +
					'		 </div>';

				$this.html(template);
			}
		};

		//generate sub items with parent id
		var loadSubLevel = function (subItems, parentId) {
			let template = '<div class="item-sub">';

			if (subItems.length > 0) {

				for (var i = 0; i < subItems.length; i++) {

					let subLevels = filterItems(subItems[i].ItemId);
					if (subLevels.length > 0) {
						let subTemplate = loadSubLevel(subLevels, subItems[i].ItemId);
						template += '<div class="item-wrapper is-parent">' +
							'			<div class="item-root item-has-child">' +
							'				<i class="zmdi zmdi-plus-square"></i>' +
							'				<span class="item-title">' + subItems[i].Name + '</span>' +
							'				<div class="item-action">' +
							'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + subItems[i].ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
							'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="' + parentId + '" data-item-id="' + subItems[i].ItemId + '">' + defaults.dic.edit + '</button>' +
							'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="' + parentId + '" data-item-id="' + subItems[i].ItemId + '">' + defaults.dic.delete + '</button>' +
							'				</div>' +
							'			</div>' +
							'			<div class="item-edit-data"></div>' +
							'			<div class="item-sub-items">' +
							subTemplate +
							'			</div>' +
							'		 </div>';
					} else {
						template += '<div class="item-wrapper">' +
							'			<div class="item-root item-no-child">' +
							'				<i class="zmdi zmdi-check-circle"></i>' +
							'				<span class="item-title">' + subItems[i].Name + '</span>' +
							'				<div class="item-action">' +
							'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + subItems[i].ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
							'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="' + parentId + '" data-item-id="' + subItems[i].ItemId + '">' + defaults.dic.edit + '</button>' +
							'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="' + parentId + '" data-item-id="' + subItems[i].ItemId + '">' + defaults.dic.delete + '</button>' +
							'				</div>' +
							'			</div>' +
							'			<div class="item-edit-data"></div>' +
							'		 </div>';
					}
				}
			}

			template += '</div>';

			return template;
		};

		//filter items with parent id
		var filterItems = function (parentId) {
			return $jsonData.filter(x => x.ParentId === parentId);
		};

		//convert inputs of an element to object
		var customSerialize = function ($wrapper) {
			let model = {};
			$wrapper.find('input:not([type="checkbox"]):not([type="radio"]),select,textarea').each(function () {
				model[$(this).attr('name')] = $(this).val();
			});

			$wrapper.find('input[type="checkbox"],input[type="radio"]').each(function () {
				let name = $(this).attr('name');
				let val = $(this).attr('value').toLowerCase();
				if (!val || val === 'true' || val === 'false') val = $(this).prop('checked');
				if (!model[name]) {
					model[name] = val;
				}
				else {
					if (Array.isArray(model[name])) model[name].push(val);
					else model[name] = [model[name], val];
				}
			});
			return model;
		};

		//Submit edit form
		var submitEditForm = function (model, $successCallback, $errorCallback) {
			if (defaults.editUrl !== '' && defaults.editUrl !== null) {
				$.post(defaults.editUrl, model)
					.done(function (rep) {
						$successCallback(rep);
					})
					.fail(function (e) {
						$errorCallback(e);
					});
			}
		};

		//Submit add form
		var submitAddForm = function (model, $successCallback, $errorCallback) {
			if (defaults.addUrl !== '' && defaults.addUrl !== null) {
				$.post(defaults.addUrl, model)
					.done(function (rep) {
						$successCallback(rep);
					})
					.fail(function (e) {
						$errorCallback(e);
					});
			}
		};

		//Filter items by name
		$(document).on('input', '#search-wrapper .search-items', function (e) {
			let $this = $(this);
			let $clearText = $this.closest('#search-wrapper').find('.clear-text');

			if ($this.val().length > 0) {
				$clearText.show();
				$('span.item-title').removeClass('highlight');
				$("span.item-title:contains('" + $this.val() + "')").addClass('highlight');
			}
			else {
				$clearText.hide();
				$('span.item-title').removeClass('highlight');
			}
		});

		//Clear filter text
		$(document).on('click', '#search-wrapper .clear-text', function (e) {
			let $this = $(this);
			$this.closest('#search-wrapper').find('.search-items').val('').focus();
			$('span.item-title').removeClass('highlight');
			$this.hide();
		});

		//open/close sub-item
		$(document).on('click', '.item-root.item-has-child i', function (e) {
			let $this = $(this);
			let $subItemWrapper = $this.closest('.item-wrapper').find('> .item-sub-items');
			if ($this.hasClass('zmdi-plus-square')) {
				$this.removeClass('zmdi-plus-square').addClass('zmdi-minus-square');
				$subItemWrapper.show();
			}
			else {
				$this.attr('class', '').addClass('zmdi zmdi-plus-square');
				$subItemWrapper.hide();
			}
		});

		//expand/all sub-items
		$(document).on('click', '.item-appender .show-all', function (e) {
			let $this = $(this);
			if ($this.hasClass('expand')) {
				defaults.autoExpand = false;
			} else {
				defaults.autoExpand = true;
			}
			expandOrCollapseTree(defaults.autoExpand);
		});

		//Add Item to Parent
		$(document).on('click', '.add-item', function (e) {
			let $elm = $(this),
				$data = {
					itemId: $elm.data('item-id'),
					parentId: $elm.data('item-pid')
				};

			$.get(defaults.formUrl, $data)
				.done(function (rep) {
					if (rep.IsSuccessful === true) {
						$this.find('.item-edit-data').html('').hide();
						let wrapper = $elm.closest('.item-wrapper').find('> .item-edit-data');
						let $html = '<div class="item-entry">' +
							rep.Body +
							'			<div class="action-wrapper">' +
							'				<button type="button" class="btn-submit-action btn-add">' + defaults.dic.btnSubmit + '</button>' +
							'				<button type="button" class="btn-cancel">' + defaults.dic.btnCancel + '</button>' +
							'			</div>' +
							'		 </div>';

						wrapper.show();
						wrapper.html($html);
					} else {
						console.log('Result is empty!');
					}
				})
				.fail(function (e) {
					console.log('Error in load form from "' + defaults.formUrl + '"');
				});
		});

		//Add Item to selected child
		$(document).on('click', '.item-action .item-add', function (e) {
			let $elm = $(this),
				$data = {
					itemId: 0,
					parentId: $elm.data('item-pid')
				};

			$.get(defaults.formUrl, $data)
				.done(function (rep) {
					if (rep.IsSuccessful === true) {
						$this.find('.item-edit-data').html('').hide();
						let wrapper = $elm.closest('.item-wrapper').find('> .item-edit-data');
						let $html = '<div class="item-entry">' +
							rep.Body +
							'			<div class="action-wrapper">' +
							'				<button type="button" class="btn-submit-action btn-add">' + defaults.dic.btnSubmit + '</button>' +
							'				<button type="button" class="btn-cancel">' + defaults.dic.btnCancel + '</button>' +
							'			</div>' +
							'		 </div>';

						wrapper.show();
						wrapper.html($html);
					} else {
						console.log('Result is empty!');
					}
				})
				.fail(function (e) {
					console.log('Error in load form from "' + defaults.formUrl + '"');
				});
		});

		//Edit selected Item
		$(document).on('click', '.item-action .item-edit', function (e) {
			let $elm = $(this),
				$data = {
					itemId: $elm.data('item-id'),
					parentId: $elm.data('item-pid')
				};

			$.get(defaults.formUrl, $data)
				.done(function (rep) {
					if (rep.IsSuccessful === true) {
						$this.find('.item-edit-data').html('').hide();
						let wrapper = $elm.closest('.item-wrapper').find('> .item-edit-data');
						let $html = '<div class="item-entry">' +
							rep.Body +
							'			<div class="action-wrapper">' +
							'				<button type="button" class="btn-submit-action btn-edit">' + defaults.dic.btnSubmit + '</button>' +
							'				<button type="button" class="btn-cancel">' + defaults.dic.btnCancel + '</button>' +
							'			</div>' +
							'		 </div>';

						wrapper.show();
						wrapper.html($html);
					} else {
						console.log('Result is empty!');
					}
				})
				.fail(function (e) {
					console.log('Error in load form from "' + defaults.formUrl + '"');
				});
		});

		//Delete selected Item
		$(document).on('click', '.item-action .item-delete', function (e) {
			let $elm = $(this);
			let params = {
				successConfirm: false,
				removeFromServer: function ($successCallback, $errorCallback) {
					if (defaults.deleteUrl !== null && defaults.deleteUrl !== '') {
						if (this.successConfirm) {
							$.post(defaults.deleteUrl, { id: $elm.data('item-id') })
								.done(function (rep) {
									if (rep.IsSuccessful) {
										$successCallback({ message: rep.Message });
									}
									else $errorCallback({ message: rep.Message });
								})
								.fail(function (e) {
									$errorCallback({ message: 'Error in delete Item!' });
								});
						}
					} else {
						throw 'Invalid DeleteUrl!';
					}
				},
				removeFromUI: function () {
					if (this.successConfirm) {
						let $wrapper = $elm.closest('.item-wrapper');
						let $parentWrapper = $wrapper.closest('.item-wrapper.is-parent');
						if ($wrapper) {
							// Remove selected item
							$wrapper.remove();

							//If parent wrapper have not a child remove parent style of parent wrapper
							if ($parentWrapper.find('.item-wrapper').length === 0) {
								$parentWrapper.removeClass('is-parent');
								$parentWrapper.find('.item-root').removeClass('item-has-child').addClass('item-no-child');
								$parentWrapper.find('i.zmdi').removeAttr('class').attr('class', 'zmdi zmdi-check-circle');
							}
						}
					}
				}
			};

			defaults.deleteSelectedItem(params);
		});

		//Cancel action on loaded form
		$(document).on('click', '.action-wrapper .btn-cancel', function (e) {
			$(this).closest('.item-edit-data').html('').hide();
			defaults.cancelForm();
		});

		//submit button click
		$(document).on('click', '.action-wrapper .btn-submit-action', function (e) {
			let $elm = $(this);
			let $actionType = $elm.hasClass('btn-edit')
				? 'edit'
				: $elm.hasClass('btn-add')
					? 'add'
					: 'unknown-type';

			let $formWrapper = $elm.closest('.item-entry');
			let serilizedInputs = customSerialize($formWrapper);

			let params = {
				isValidForm: false,
				submitForm: function ($successCallback, $errorCallback) {
					if (this.isValidForm) {
						if ($actionType === 'edit') {
							//Call Edit
							submitEditForm(serilizedInputs, function (result) {
								if (result.IsSuccessful) {
									let $title = $elm.closest('.item-wrapper').find('> .item-root .item-title');
									$title.text(result.Result.Name);
									$successCallback({ message: result.Message });
								} else {
									$errorCallback({ message: result.Message });
								}
							}, function (err) {
								$errorCallback({ message: 'Error in submit form!' });
							});

						} else if ($actionType === 'add') {
							//Call Add
							submitAddForm(serilizedInputs, function (result) {
								if (result.IsSuccessful) {
									let $wrapper = $elm.closest('.item-wrapper');
									let template = '';
									if (result.Result.ParentId === null || result.Result.ParentId === 0) {
										template = '<div class="item-wrapper">' +
											'			<div class="item-root item-no-child">' +
											'				<i class="zmdi zmdi-check-circle"></i>' +
											'				<span class="item-title">' + result.Result.Name + '</span>' +
											'				<div class="item-action">' +
											'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
												'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.edit + '</button>' +
													'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.delete + '</button>' +
											'				</div>' +
											'			</div>' +
											'			<div class="item-edit-data" style="display: none;"></div>' +
											'		</div>';

										$this.find('> .item-wrapper:last').before(template);
									} else if (!$wrapper.hasClass('is-parent')) {
										$wrapper.addClass('is-parent');
										$wrapper.find('> .item-root').removeAttr('class').attr('class', 'item-root item-has-child');
										$wrapper.find('> .item-root i.zmdi').removeAttr('class').attr('class', 'zmdi zmdi-minus-square');
										$wrapper.append('<div class="item-sub-items" style="display: block;"></div>');
										template = '<div class="item-sub">' +
											'			<div class="item-wrapper">' +
											'				<div class="item-root item-no-child">' +
											'					<i class="zmdi zmdi-check-circle"></i>' +
											'					<span class="item-title">' + result.Result.Name + '</span>' +
											'					<div class="item-action">' +
											'						<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
											'						<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.edit + '</button>' +
											'						<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.delete + '</button>' +
											'					</div>' +
											'				</div>' +
											'				<div class="item-edit-data" style="display: none;"></div>' +
											'			</div>' +
											'		</div>';

										$wrapper.find('> .item-sub-items').prepend(template);
									} else {
										template = '<div class="item-wrapper">' +
											'			<div class="item-root item-no-child">' +
											'				<i class="zmdi zmdi-check-circle"></i>' +
											'				<span class="item-title">' + result.Result.Name + '</span>' +
											'				<div class="item-action">' +
											'					<button type="button" class="item-add ' + (defaults.accessibility.showAddSubItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ItemId + '" data-item-id="0">' + defaults.dic.addSubItem + '</button>' +
											'					<button type="button" class="item-edit ' + (defaults.accessibility.showEditItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.edit + '</button>' +
											'					<button type="button" class="item-delete ' + (defaults.accessibility.showDeleteItem === true ? '' : 'hide') + '" data-item-pid="' + result.Result.ParentId + '" data-item-id="' + result.Result.ItemId + '">' + defaults.dic.delete + '</button>' +
											'				</div>' +
											'			</div>' +
											'			<div class="item-edit-data" style="display: none;"></div>' +
											'		</div>';

										$wrapper.find('> .item-sub-items').find('.item-sub').prepend(template);
									}

									$successCallback({ message: result.Message });
								} else {
									$errorCallback({ message: result.Message });
								}
							}, function (err) {
								$errorCallback({ message: 'Error in submit form!' });
							});

						}
						else {
							throw 'Invalid type for submit!';
						}
					}
				},
				hideForm: function () {
					$elm.closest('.item-edit-data').html('').hide();
				}
			};

			defaults.submitForm(params);
		});

		//Initilize PlugIn
		_init();

		return this;
	};
})(jQuery);