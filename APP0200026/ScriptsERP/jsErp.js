var ServerURL = "";
$(document).ready(function () {
    jQuery.ajaxSetup({ cache: false });
    //setInterval(function () { UpdateRate(); }, 20000);
});

function UpdateRate() {
    jQuery.ajaxSetup({ cache: false });
    var url = ServerURL + "Public/Update_Rate";
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //alert(item._Status);
    });
}

function jsEbank_SessionLanguage(iMaNgonNgu) {
    $.ajaxSetup({ cache: false });
    var url = ServerURL + "Public/Check_Language?iID_MaNgonNgu=" + iMaNgonNgu;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        var _sCode = item._sCode;
        var imgLanguage = document.getElementById("imgLanguage");
        imgLanguage.src = ServerURL + "/XML/" + _sCode + ".gif";

        //var div_result = document.getElementById("spanLanguage");
        //div_result.innerHTML = item._sLanguage;

        setTimeout("window.location.reload();", 1000);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsEbank_Create_Code_SanPham(chkCheck, txtValue) {
    if (chkCheck.checked == true) {
        var url = ServerURL + "/Public/Create_Code_SanPham";
        jQuery.getJSON(url, function (item) {
            if (item == null) {
                return null;
            }

            document.getElementById(txtValue).value = item._Code;
        });
    }
    else {
        document.getElementById(txtValue).value = "";
    }
}

function loadCaptcha() {
    var sURL = ServerURL + "Public/generateCaptcha";
    $.ajax({
        type: 'GET', url: sURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            $("#m_imgCaptcha").attr('src', ServerURL + data);
        },
        error: function (data) { alert("Error while loading captcha image") }
    });
}

function checkAllGroupItem(groupID, obj) {
    var objValue = document.getElementById(obj).value;
    if (!isNaN(groupID)) {
        if (objValue != "0") {
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if ((document.forms[0].elements[i].type == "checkbox") && (document.forms[0].elements[i].id == groupID)) {
                    document.forms[0].elements[i].checked = true;
                }
            }
            document.getElementById(obj).value = "0";
        }
        else {
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if ((document.forms[0].elements[i].type == "checkbox") && (document.forms[0].elements[i].id == groupID)) {
                    document.forms[0].elements[i].checked = false;
                }
            }
            document.getElementById(obj).value = "1";
        }
    }
}