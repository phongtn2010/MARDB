var ServerURL = "";
//Dialog Tao Chung Tu Cap Thu
var jsDialog_ShowDialogCapThu_TieuDe = true;
function jsDialog_ShowDialogCapThu() {
    var title = "Thêm chứng từ duyệt cấp phát";
    var dWidth = 702;
    var dHeight = 380;

    //Doi tuong tren form
    var _MaCapPhat = $("#KTTG_ChungTuCapThu_Duyet_dialog_iID_MaCapPhat");
    var _SoChungTu = $("#KTTG_ChungTuCapThu_Duyet_dialog_sSoChungTu");
    var _TaiKhoanNo = $("#KTTG_ChungTuCapThu_Duyet_dialog_iID_MaTaiKhoan_No");
    var _TaiKhoanCo = $("#KTTG_ChungTuCapThu_Duyet_dialog_iID_MaTaiKhoan_Co");
    var _DonViNo = $("#KTTG_ChungTuCapThu_Duyet_dialog_iID_MaDonVi_No");
    var _DonViCo = $("#KTTG_ChungTuCapThu_Duyet_dialog_iID_MaDonVi_Co");
    var _NoiDungChungTu = $("#KTTG_ChungTuCapThu_Duyet_dialog_sNoiDung");
    var allFields = $([]).add(_MaCapPhat).add(_SoChungTu).add(_TaiKhoanNo).add(_TaiKhoanCo).add(_DonViNo).add(_DonViCo)
    var tips = $(".validateTips");    

    function updateTips(t) {
        tips
            .text(t)
            .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }
    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Chiều dài của " + n + " phải được nhập từ " + min + " và " + max + ".");
            return false;
        } else {
            return true;
        }
    }
    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }
    function checkValueSelect(o, n, v) {
        if (o.val() == v) {
            o.addClass("ui-state-error");
            updateTips("Bạn phải chọn giá trị của " + n + ".");
            return false;
        } else {
            return true;
        }
    }

    $("#idDialog_ChungTuCapThu").dialog({
        width: dWidth,
        height: dHeight,
        modal: true,
        title: title,
        resizable: true,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "explode",
            duration: 500
        },
        open: function () {
            //$(".ui-dialog").css("background", "transparent");
            //$(".ui-dialog").css("border", "none");
            //$("#wrap-giaodich").css("background", "url(" + strPopup + ") no-repeat scroll 0 0 transparent");
        },
        buttons: {
            "Tạo mới": function () {
                var jsURL_AddNew = "#";
                //validate du lieu
                var bValid = true;
                allFields.removeClass("ui-state-error");
                bValid = bValid && checkValueSelect(_MaCapPhat, "chứng từ cấp phát", "");
                bValid = bValid && checkLength(_SoChungTu, "số ủy nhiệm chi", 1, 10);
                bValid = bValid && checkValueSelect(_TaiKhoanNo, "tài khoản nợ", "00000000-0000-0000-0000-000000000000");
                bValid = bValid && checkValueSelect(_TaiKhoanCo, "tài khoản có", "00000000-0000-0000-0000-000000000000");
                bValid = bValid && checkValueSelect(_DonViNo, "đơn vị nợ", "");
                bValid = bValid && checkValueSelect(_DonViCo, "đơn vị có", "");
                if (bValid) {
                    //Xu ly thong  tin co so du lieu
                    var url = ServerURL + "KTCT_DuyetCapThu/TaoChungTu_CapThu?_MaCapPhat=" + _MaCapPhat.val();
                    url += "&_SoChungTu=" + _SoChungTu.val();
                    url += "&_TaiKhoanNo=" + _TaiKhoanNo.val();
                    url += "&_TaiKhoanCo=" + _TaiKhoanCo.val();
                    url += "&_DonViNo=" + _DonViNo.val();
                    url += "&_DonViCo=" + _DonViCo.val();
                    url += "&_NoiDungChungTu=" + _NoiDungChungTu.val();
                    $.getJSON(url, function (item) {
                        if (item != null && item != "") {
                            jsURL_AddNew = item._url;
                        }
                    });
                    $(this).dialog("close");
                    window.location.href = jsURL_AddNew;
                }
            },
            "Hủy": function () {
                $(this).dialog("close");
            }
        }
    });
    return false;
}

//Dialog Tao Chung Tu Du Toan
var jsDialog_ShowDialogDuToanChungTu_TieuDe = true;
function jsDialog_ShowDialogDuToanChungTu() {
    var title = "Thêm chứng từ dự toán";
    var dWidth = 702;
    var dHeight = 250;
    var _MaDotNganSach = $("#DuToan_ChungTu_MaDotNganSach");
    var _MaLoaiNganSach = $("#DuToan_ChungTu_iID_MaLoaiNganSach");
    var _NghanhDonVi = $("#DuToan_ChungTu_NghanhDonVi");
    var _NgayChungTu = $("#DuToan_ChungTu_vidNgayChungTu");
    var _NoiDungChungTu = $("#DuToan_ChungTu_sNoiDung");
    var allFields = $([]).add(_NgayChungTu);
    var tips = $(".validateTips");

    function updateTips(t) {
        tips
            .text(t)
            .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }
    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Chiều dài của " + n + " phải được nhập từ " + min + " và " + max + ".");
            return false;
        } else {
            return true;
        }
    }
    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }

    $("#idDialog_DuToanChungTu").dialog({
        width: dWidth,
        height: dHeight,
        modal: true,
        title: title,
        resizable: true,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "explode",
            duration: 500
        },
        open: function () {
            //$(".ui-dialog").css("background", "transparent");
            //$(".ui-dialog").css("border", "none");
            //$("#wrap-giaodich").css("background", "url(" + strPopup + ") no-repeat scroll 0 0 transparent");
        },
        buttons: {
            "Tạo mới": function () {
                var jsURL_AddNew = "#";
                //validate du lieu
                var bValid = true;
                allFields.removeClass("ui-state-error");
                bValid = bValid && checkLength(_NgayChungTu, "ngày chứng từ", 1, 10);
                if (bValid) {
                    //Xu ly thong  tin co so du lieu
                    var url = ServerURL + "DuToan_ChungTu/TaoChungTu_CoLoaiNganSach?_MaDotNganSach=" + _MaDotNganSach.val();
                    url += "&_MaLoaiNganSach=" + _MaLoaiNganSach.val();
                    url += "&_NghanhDonVi=" + $("input:checked").val();
                    url += "&_NgayChungTu=" + _NgayChungTu.val();
                    url += "&_NoiDungChungTu=" + _NoiDungChungTu.val();
                    $.getJSON(url, function (item) {
                        if (item != null && item != "") {
                            jsURL_AddNew = item._url;
                        }
                    });
                    $(this).dialog("close");
                    window.location.href = jsURL_AddNew;
                }
            },
            "Hủy": function () {
                $(this).dialog("close");
            }
        }
    });
    return false;
}