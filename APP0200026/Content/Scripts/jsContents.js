var ServerUrl = "";
var jsMyip_Client = "";

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

function jsBooks_SessionLanguage(iMaNgonNgu) {
    $.ajaxSetup({ cache: false });
    var url = ServerUrl + "/Language/Check_Language?iID_MaNgonNgu=" + iMaNgonNgu;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        var _sCode = item._sCode;
        var imgLanguage = document.getElementById("imgLanguage");
        imgLanguage.src = ServerUrl + "/XML/" + _sCode + ".gif";

        //var div_result = document.getElementById("spanLanguage");
        //div_result.innerHTML = item._sLanguage;


        setTimeout("window.location.reload();", 1000);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsBVND_SessionLanguage_Home(iMaNgonNgu) {
    $.ajaxSetup({ cache: false });
    var url = ServerUrl + "/Language/Check_Language?iID_MaNgonNgu=" + iMaNgonNgu;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        var _sCode = item._sCode;

        setTimeout("window.location.reload();", 1000);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsEbank_AddToCart(sMaSanPhan, sCode, rSoLuong, rDonGia) {
    var url = ServerUrl + "/Orders/Insert_User_Cart?iID_MaSanPham=" + sMaSanPhan + "&sCodeSanPham=" + sCode + "&rSoLuong=" + rSoLuong + "&rDonGia=" + rDonGia;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
        //HT lại luot đat
        jsEbank_Count_Cart();
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_UpdateToCart(sMaGioHang_Temp, iSoLuong) {
    var url = ServerUrl + "/Orders/Update_User_Cart?iID_GioHang_Temp=" + sMaGioHang_Temp + "&iSoLuong=" + iSoLuong;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
    });
}

function jsEbank_DeleteToCart(sMaGioHang_Temp) {
    var url = ServerUrl + "/Orders/Delete_User_Cart?iID_GioHang_Temp=" + sMaGioHang_Temp;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
        //HT lại luot đat
        jsEbank_Count_Cart();
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_Count_Cart() {
    var url = ServerUrl + "/Orders/Get_Count_User_Cart";
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        if (item.sCode == 1) {
            document.getElementById("spanCount").innerHTML = item.sMess;
            var spanSumMoney = document.getElementById("spanSumMoney");
            spanSumMoney.innerHTML = item.sSum;

            document.getElementById("spanCount1").innerHTML = item.sMess;
            var spanSumMoney1 = document.getElementById("spanSumMoney1");
            spanSumMoney1.innerHTML = item.sSum;

            var div_result = document.getElementById("listProductOrder");
            div_result.innerHTML = item.sHTML;

        }
        
    });
}

function jsEbank_Cart_VanDon(iGioHang, divID) {
    var url = ServerUrl + "/Orders/Get_User_Cart_VanDon?iID_MaGioHang=" + iGioHang;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        if (item.sCode == 1) {
            var div_result = document.getElementById(divID);
            div_result.innerHTML = item.sHTML;
        }
    });
}


function jsEbank_AddToFevorites(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Insert_User_Like?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_DeleteToFevorites(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Delete_User_Like?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
        window.location = ServerUrl + "/Members/Favorites";
    });
}

function jsEbank_AddToCompare(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Insert_User_Compare?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_DeleteToCompare(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Delete_User_Compare?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
        window.location = ServerUrl + "/Members/Compare";
    });
}

function jsEbank_AddToDanhGia(sMaSanPhan, iSao, sHoTen, sEmail, sNoiDung) {
    var url = ServerUrl + "/Orders/Insert_User_DanhGia?iID_MaSanPham=" + sMaSanPhan + "&iSao=" + iSao + "&sHoTen=" + sHoTen + "&sEmail=" + sEmail + "&sNoiDung=" + sNoiDung;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}