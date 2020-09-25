(function () {
    this.ajax = function (params) {
        params.loading = (typeof params.loading !== 'undefined' && !params.loading) ? false : true;
        if (params.contentType === 'json') {
            params.contentType = 'application/json; charset=utf-8';
            params.data = JSON.stringify(params.data);
            dataType: 'json';
        }
        params = $.extend({
            url: params.service,
            success: function (result) {
                if (params.loading)
                    loading.hide();
                params.done(result);
            },
            error: function () {
                if (params.loading)
                    loading.hide();
            }
        }, params);
        if (params.loading)
            loading.show();
        setTimeout(function () {
            $.ajax(params);
        }, 300);
    };
    this.ajaxSubmit = function (params) {
        //for (var obj in CKEDITOR.instances) {
        //    CKEDITOR.instances[obj].updateElement();
        //}
        console.log($('#' + params.id).serialize());
        //var data = {};
        //$('#' + params.id).find('input, select, textarea').each(function() {
        //    if ($(this).attr('type') === 'checkbox') {
        //        if ($(this).is(':checked')) {
        //            data[$(this).attr('name')] = true;
        //        } else {
        //            data[$(this).attr('name')] = false;
        //        }
        //    } else if ($(this).attr('type') === 'radio') {
        //        if ($(this).is(':checked')) {
        //            data[$(this).attr('name')] = $(this).val();
        //        }
        //    } else {
        //        if ($(this).val() !== '' && typeof $(this).attr('name') != 'undefined') {
        //            if (isNaN($(this).val())) {
        //                data[$(this).attr('name')] = $(this).val();
        //            } else {
        //                data[$(this).attr('name')] = parseFloat($(this).val());
        //            }
        //        }
        //    }
        //});
        para = $.extend({
            success: function (result) {
                loading.hide();
                if (result.success) {
                    params.done(result);
                } else {
                    if (result.data) {
                        $('#' + params.id + ' input,#' + params.id + ' select,#' + params.id + ' textarea').each(function () {
                            $(this).parents('.form-groups').removeClass('has-error');
                            $(this).next('.help-block').remove();
                            if ($(this).attr('name') && result.data[$(this).attr('name')]) {
                                $(this).parents('.form-groups').addClass('has-error');
                                $(this).after('<span class="help-block">' + result.data[$(this).attr('name')] + '</span>');
                            }
                        });
                    }
                    if (result.message && !params.hideMess) {
                        popup.msg(result.message);
                    }
                }
            },
            service: params.service,
            type: 'post',
            data: $('#' + params.id).serialize()//data
        }, params);
        ajax(para);
    };
    this.ajaxUpload = function (params) {
        //for (var obj in CKEDITOR.instances) {
        //    CKEDITOR.instances[obj].updateElement();
        //}
        params.loading = (typeof params.loading !== 'undefined' && !params.loading) ? false : true;
        if (params.loading) {
            loading.show();
        }
        var action = baseUrl + params.service;
        if ($('#upload-iframe-submit').length > 0) {
            $('#upload-iframe-submit').remove();
        }
        $('body').append('<iframe id="upload-iframe-submit" name="upload-iframe-submit" style="display:none" />');
        $('#' + params.id).attr('target', 'upload-iframe-submit');
        $('#' + params.id).attr('action', action);
        $('#' + params.id).attr('method', 'post');
        $('#' + params.id).attr('enctype', 'multipart/form-data');
        $('#' + params.id).submit();
        $('#upload-iframe-submit').load(function () {
            try {
                var result = $('#upload-iframe-submit').contents().find('body');
                result = $.parseJSON(result.text());
                if (!result.success && result.message === '-signin') {
                    viewer = null;
                    document.location = '#auth';
                }
                setTimeout(function () {
                    if (params.loading) {
                        loading.hide();
                    }
                    if (result.success) {
                        params.done(result);
                    } else {
                        if (result.data) {
                            $('#' + params.id + ' input,#' + params.id + ' select,#' + params.id + ' textarea').each(function () {
                                $(this).parents('.form-group').removeClass('has-error');
                                $(this).next('.help-block').remove();
                                if ($(this).attr('name') && result.data[$(this).attr('name')]) {
                                    $(this).parents('.form-group').addClass('has-error');
                                    $(this).after('<span class="help-block">' + result.data[$(this).attr('name')] + '</span>');
                                }
                            });
                        }
                        if (result.message) {
                            popup.msg(result.message);
                        }
                    }
                }, 300);
            } catch (err) {
                if (params.loading) {
                    loading.hide();
                }
            }
        });
    };
    this.editor = function (id, options) {
        options = $.extend({
            autoGrow_maxHeight: 400
        }, options);
        setTimeout(function () {
            CKEDITOR.replace(id, options);
            CKEDITOR.on('instanceReady', function () { });
        }, 500);
    };
})()
    +
    (function () {
        function Popup() {
            this.open_Content = function (id, title, content, cmd, type, backdrop, button_close) {

                if ($('#' + id).length > 0) {
                    $('#' + id).remove();
                }
                if (type === '') {
                    type = '';
                }
                var buttonCloseFag = false;
                var buttonClose = '<button type="button" class="close detail-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span><span class="close-text">Đóng lại</span></button>';
                if (typeof button_close !== 'undefined' && button_close == true) {
                    buttonClose = '';
                    buttonCloseFag = true;
                }
                $('body:first').append('<div class="modal fade" id="' + id + '">\
                <div class="modal-dialog ' + type + '">\
                    <div class="modal-content">\
                        <div class="modal-header">\
                            ' + buttonClose + '\
                            ' + (title === null || title === '' ? '' : '<div class="checkout-title"><div class="lb-name ship-title">' + title + '</div></div>') + '\
                        </div>\
                        <div class="modal-body clearfix" id="myModalContent">' + content + '</div>\
                        <div class="modal-footer"></div>\
                    </div>\
                </div>\
            </div>');
                $('#' + id + ' .close').click(function () {
                    popup.close(id);
                });
                if (cmd) {
                    for (var i = 0; i < cmd.length; i++) {
                        $('#' + id + ' .modal-footer').append('<button type="button" class="btn ' + cmd[i].style + '" id="' + 'popup-cmd-' + id + '-' + i + '">' + cmd[i].title + '</button>');
                        $('#' + 'popup-cmd-' + id + '-' + i).click(cmd[i].fn);
                    }
                }
                var option = {};
                if (typeof backdrop !== 'undefined' && backdrop == true) {
                    option.backdrop = 'static';
                }

                $('#' + id).modal(option);
                if (!buttonCloseFag) {
                    $('body').keydown(function (e) {
                        if (e.keyCode === 27) {
                            popup.close(id);
                        }
                    });
                }
                $(".modal-open").css("padding-right", "0px");
            };
            this.open = function (id, title, content_link, cmd, type, backdrop, button_close) {

                if ($('#' + id).length > 0) {
                    $('#' + id).remove();
                }
                if (type === '') {
                    type = '';
                }
                var buttonCloseFag = false;
                var buttonClose = '<button type="button" class="close detail-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span><span class="close-text">Đóng lại</span></button>';
                if (typeof button_close !== 'undefined' && button_close == true) {
                    buttonClose = '';
                    buttonCloseFag = true;
                }
                $('body:first').append('<div class="modal fade" id="' + id + '">\
                <div class="modal-dialog ' + type + '">\
                    <div class="modal-content">\
                        <div class="modal-header">\
                            ' + buttonClose + '\
                            ' + (title === null || title === '' ? '' : '<div class="checkout-title"><div class="lb-name ship-title">' + title + '</div></div>') + '\
                        </div>\
                        <div class="modal-body clearfix" id="myModalContent"></div>\
                        <div class="modal-footer"></div>\
                    </div>\
                </div>\
            </div>');
                $('#' + id + ' .close').click(function () {
                    popup.close(id);
                });
                if (cmd) {
                    for (var i = 0; i < cmd.length; i++) {
                        $('#' + id + ' .modal-footer').append('<button type="button" class="btn ' + cmd[i].style + '" id="' + 'popup-cmd-' + id + '-' + i + '">' + cmd[i].title + '</button>');
                        $('#' + 'popup-cmd-' + id + '-' + i).click(cmd[i].fn);
                    }
                }
                var option = {};
                if (typeof backdrop !== 'undefined' && backdrop == true) {
                    option.backdrop = 'static';
                }

                $('#myModalContent').load(content_link, function () {
                    $('#' + id).modal(option);
                });
                if (!buttonCloseFag) {
                    $('body').keydown(function (e) {
                        if (e.keyCode === 27) {
                            popup.close(id);
                        }
                    });
                }
                $(".modal-open").css("padding-right", "0px");
            };
            this.open_contenttemplate = function (id, title, content, cmd, type, backdrop, button_close) {
                if ($('#' + id).length > 0) {
                    $('#' + id).remove();
                }
                if (type === '') {
                    type = '';
                }
                var buttonCloseFag = false;
                var buttonClose = '<button type="button" class="close detail-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span><span class="close-text">Đóng lại</span></button>';
                if (typeof button_close !== 'undefined' && button_close == true) {
                    buttonClose = '';
                    buttonCloseFag = true;
                }
                $('body:first').append('<div class="modal fade in" id="' + id + '">\
                <div class="modal-dialog ' + type + '">\
                    <div class="modal-content">\
                        <div class="modal-header">\
                            ' + buttonClose + '\
                            ' + (title === null || title === '' ? '' : '<div class="checkout-title"><div class="lb-name ship-title">' + title + '</div></div>') + '\
                        </div>\
                        <div class="modal-body" id="myModalContent">' + content + '</div>\
                        <div class="modal-footer"></div>\
                    </div>\
                </div>\
            </div>');
                $('#' + id + ' .close').click(function () {
                    popup.close(id);
                });
                if (cmd) {
                    for (var i = 0; i < cmd.length; i++) {
                        $('#' + id + ' .modal-footer').append('<button type="button" class="btn ' + cmd[i].style + '" id="' + 'popup-cmd-' + id + '-' + i + '">' + cmd[i].title + '</button>');
                        $('#' + 'popup-cmd-' + id + '-' + i).click(cmd[i].fn);
                    }
                }
                var option = {};
                if (typeof backdrop !== 'undefined' && backdrop == true) {
                    option.backdrop = 'static';
                }
                var dialogWidth = 800;
                $('#' + id).dialog({
                    modal: true,
                    title: title,
                    width: dialogWidth,
                    resizable: true,
                    dialogClass: 'customer-success-dialog',
                    position: [($(window).width() / 2) - (dialogWidth / 2), 150],
                    create: function (event, ui) {
                        var widget = $(this).dialog("widget");
                        $(".ui-dialog-titlebar", widget).remove();
                    }
                });
                // $('#' + id).modal(option);
                if (!buttonCloseFag) {
                    $('body').keydown(function (e) {
                        if (e.keyCode === 27) {
                            popup.close(id);
                        }
                    });
                }
                //$(".modal-open").css("padding-right", "0px");
            };
            this.close = function (id) {
                $('#' + id).modal('hide');
                $('#' + id).remove();
                var hasModal = false;
                $('.modal').each(function () {
                    if ($(this).is(":visible")) {
                        hasModal = true;
                    }
                });
                if (!hasModal) {
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                }
            };
            this.msg = function (msg, fn) {
                this.open_Content('popup-msg', 'Thông báo', '<div style="min-width: 300px;padding-left: 18px;">' + msg + '</div>', [{
                    title: "Đồng ý",
                    style: "btn-primary",
                    fn: function () {
                        if (fn) {
                            fn();
                        }
                        popup.close('popup-msg');
                    }
                }]);
            };
            this.msgFix = function (msg, fn) {
                this.open('popup-msg', 'Thông báo', '<div style="min-width: 300px;padding-left: 18px;">' + msg + '</div>', [{
                    title: "Đồng ý",
                    style: "btn-primary",
                    fn: function () {
                        if (fn) {
                            fn();
                        }
                        popup.close('popup-msg');
                    }
                }], '', true, true);
            };
            this.confirm = function (msg, fn) {
                this.open('popup-confirm', 'Xác nhận', '<div class="container" style="min-width: 300px;padding-left: 18px;">' + msg + '</div>', [{
                    title: "Đồng ý",
                    style: "btn-primary",
                    fn: function () {
                        fn();
                        popup.close('popup-confirm');
                    }
                }, {
                    title: 'Từ chối',
                    fn: function () {
                        popup.close('popup-confirm');
                    }
                }], '', true, true);
            };
        }
        function Loading() {
            this.show = function () {
                if ($('#loading').length === 0) {
                    var html = '<div id="loading" class="modal fade" style=" z-index:1100">\
                <div class="modal-dialog" style="margin: 130px auto;">\
                    <div class="modal-content" style="border-radius: 0px;border-top: 3px solid #d95252;">\
                        <div class="modal-body" style="padding: 13px 0px;">\
                            <div class="loading" style="text-align: center;">\
                                <div class="loading-img"></div><p>Vui lòng chờ trong giây lát!</p></div>\
                            </div>\
                        </div>\
                    </div>\
                </div>';
                    var html2 = '<div id="loading" style="position: fixed;width: 100%;height: 100%;top:0;left: 0;background: #000 url(/content/images/spin_main.svg) no-repeat 50% 50%;opacity: 0.45;filter:alpha(opacity=45);z-index: 9999;"></div>';
                    $('body:first').append(html2);
                }
                $('#loading').modal();
            };
            this.hide = function () {
                popup.close("loading");
            };
        }
        this.loading = new Loading();
        this.popup = new Popup();
    })();
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }

    return data;
};
function showerror(errors) {
    var content = "";
    if (errors === undefined) return;
    content += "<ul style=\"color:red;margin-left:15px;\">";
    for (var i = 0; i < errors.length; i++) {
        content += "<li>- " + errors[i].Value[0] + "</li>";
    }
    content += "</ul>";
    return content;
}
function DisplayErrors(errors) {
    
    if (errors !== undefined) {
        for (var i = 0; i < errors.length; i++) {
            if (errors[i].Key === undefined) {
                $("span[data-valmsg-for]").html('');
                $("span[data-valmsg-for]")
                    .html(errors);
                return;
            }
            $("span[data-valmsg-for=\"" + errors[i].Key + "\"]").html('');
            $("span[data-valmsg-for='" + errors[i].Key + "']").addClass("field-validation-error field-validation-error-c")
                .html(errors[i].Value[0]);
        }
        $("#" + errors[0].Key).select().focus();
    }
}
/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
(function ($, AdminLTE) {

    "use strict";
    $(document).ready(function () {
        $('#exTab2 ul li a').click(function (ev) {
            $('#exTab2 ul li').removeClass('selected');
            $(ev.currentTarget).parent('li').addClass('selected');
        });
    });
  
    /**
     * Store a new settings in the browser
     *
     * @param String name Name of the setting
     * @param String val Value of the setting
     * @returns void
     */
    function store(name, val) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem(name, val);
        } else {
            window.alert('Please use a modern browser to properly view this template!');
        }
    }

    /**
     * Get a prestored setting
     *
     * @param String name Name of of the setting
     * @returns String The value of the setting | null
     */
    function get(name) {
        if (typeof (Storage) !== "undefined") {
            return localStorage.getItem(name);
        } else {
            window.alert('Please use a modern browser to properly view this template!');
        }
    }

    /**
     * Retrieve default settings and apply them to the template
     *
     * @returns void
     */
  
    $("#btnCopyQr").mouseleave(function () {
        var $event = $(this);
        $event.text("Sao chép");
    });
})(jQuery, $.AdminLTE);

function changeInputHND(event) {
    var $this = $(event);
    var TyGia = $('#sTyGia').val();
    var val = $this.val().replace(/\./g, '');
    $('#sUSDAmount_show').val(parseFloat(val * TyGia).toMoney(0, ',', '.'));
    $('#sUSDAmount').val(parseFloat(val * TyGia));
}
function copyToClipboard(event, element) {
    var $event = $(event);
    var $this = $(element);
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($this.text()).select();
    document.execCommand("copy");
    $temp.remove();
    $event.text("Đã sao chép");
}
function outCopyToClipboard(event) {
    var $event = $(event);
    $event.text("Sao chép");
}
Number.prototype.toMoney = function (decimals, decimal_sep, thousands_sep) {
    var n = this,
        c = isNaN(decimals) ? 2 : Math.abs(decimals),
        d = decimal_sep || '.',
        t = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        sign = (n < 0) ? '-' : '',
        i = parseInt(n = Math.abs(n).toFixed(c)) + '',
        j = ((j = i.length) > 3) ? j % 3 : 0;
    return sign + (j ? i.substr(0, j) + t : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : '');
};
function bindBootstrapTabSelectEvent(tabsId) {
    $('#' + tabsId + ' > ul li a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var tabName = $(e.target).attr("data-tab-name");
        $(".selected-tab-name").val(tabName);
    });
}
(function ($) {
    "use strict";
    $(document).ready(function () {
        $(".withdraw_wallet").click(function () {
            ajax({
                service: "/Payment/RutHoaHong",
                type: 'POST',
                loading: true,
                done: function (resp) {
                    if (resp.success) {
                        popup.open_Content('popup-ruthoahong', 'Rút lãi hệ thống về ví', resp.html, [{
                            title: "Hủy bỏ",
                            style: "btn-default",
                            fn: function () {
                                popup.close('popup-ruthoahong');
                            }
                        }, {
                            title: 'Đồng ý', style: 'btn-danger',
                            fn: function () {
                                var serializedForm = $('#fromDraw').serialize();
                                var url = $('#fromDraw').attr("action");
                                addAntiForgeryToken(serializedForm);
                                ajax({
                                    cache: false,
                                    type: 'POST',
                                    url: url,
                                    data: serializedForm,
                                    dataType: 'json',
                                    done: function (data) {
                                        if (data.success) {
                                            popup.msg(data.message, function () {
                                                location.reload(true);
                                            });
                                        }
                                        else {
                                            //console.log(data.error);
                                            DisplayErrors(data.error);
                                            popup.msg(showerror(data.error));
                                        }
                                    },
                                    error: function () {

                                    }
                                });
                            }
                        }], 'modal-md', true);
                    } else {
                        popup.msg(resp.message);
                    }
                },
                error: function (xhr) {
                    console.log(xhr);
                    popup.msg(xhr.responseText);
                }
            });
        });
    });
})(jQuery);