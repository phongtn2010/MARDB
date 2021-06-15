$(function () {
    $("body").on("click", ".opentuchoi", function () {

        var iID_MaHoSo = $(this).data("id");
        var sMaHoSo = $(this).data("mahoso");
        $("#TC_iID_MaHoSo").val(iID_MaHoSo);
        $("#mahoso").html(sMaHoSo);

        var obj = {"Confirm": true, "IdHoso": iID_MaHoSo};
        kyso("txtSignTuChoi", obj);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var bootstrapValidator = $("#formTuChoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formTuChoi")[0]);
            $.ajax({
                url: ServerUrl + '/LanhDaoCuc/TuChoiHoSoSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#TuChoi').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#TuChoi').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});

$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHoSo = $("#Detail_iID_MaHoSo").val();
        $.ajax({
            url: ServerUrl + '/LanhDaoCuc/Thoat',
            type: 'POST',
            data: { iID_MaHoSo: iID_MaHoSo},
            success: function (response) {
                if (response.success) {
                    showToast_Success();
                    location.href = response.value;
                }
            },
            error: function (response) {
                showToast_Error();
            }
        });
    });
});


///Hang hoa


$(function () {
    $("body").on("click", "#btnDongY", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.confirm({
            title: "THÔNG BÁO XÁC NHẬN",
            text: "Bạn có chắc chắn đồng ý không?",
            confirm: function (button) {
                $.ajax({
                    url: ServerUrl + '/LanhDaoCuc/DongY',
                    type: 'POST',
                    data: { iID_MaHangHoa: iID_MaHangHoa },
                    success: function (response) {
                        if (response.success) {
                            showToast_Success();
                            location.href = response.value;
                        }
                    },
                    error: function (response) {
                        showToast_Error();
                    }
                });
            },
            cancel: function (button) {
                return false;
                //alert("Bạn đã từ chối tiếp nhận!");
            },
            confirmButton: "Đồng ý",
            cancelButton: "Từ chối"
        });
    });
});
$(function () {
    $("body").on("click", ".opentuchoiHH", function () {
        var iID_MaHangHoa = $(this).data("id");
        $("#TC_iID_MaHangHoa").val(iID_MaHangHoa);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoiHH", function () {
        var bootstrapValidator = $("#formTuChoiHH").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formTuChoiHH")[0]);
            $.ajax({
                url: ServerUrl + '/LanhDaoCuc/TuChoiHoSoHHSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#TuChoiHH').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#TuChoiHH').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});

$(function () {
    $("body").on("click", "#btnThoatHH", function () {
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoa").val();
        $.ajax({
            url: ServerUrl + '/LanhDaoCuc/ThoatHH',
            type: 'POST',
            data: { iID_MaHangHoa: iID_MaHangHoa },
            success: function (response) {
                if (response.success) {
                    showToast_Success();
                    location.href = response.value;
                }
            },
            error: function (response) {
                showToast_Error();
            }
        });
    });
});

$(document).on('keypress', function (e) {
    if (e.which === 13) {
        checkvalidAndSubmit();
    }
});

$(function () {
    $("body").on("click", "#btnSearch", function () {
        checkvalidAndSubmit();
    });
});
//End Hang hoa



function kyso(idobj, obj) {
    try {
        const xmlData = OBJtoXML(obj);
        const toBase64 = btoa(xmlData);
        console.log(toBase64)
        create(idobj, "xml", toBase64, response => {
            console.log(response)
        })

        return true;
    }
    catch (err) {
        return false;
    }
}

function getRequest() {
    var requestReq;
    try {
        requestReq = new XMLHttpRequest();
    } catch (ex) {
        requestReq = (Object.getOwnPropertyDescriptor && Object.getOwnPropertyDescriptor(window, "ActiveXObject")) ||
            ("ActiveXObject" in window);
    }
    return requestReq;
}

function create(idobj, extension, fileData, uploadUrl, onSuccess, onError) {
    var jsonBody = {
        "extension": extension,
        "fileData": fileData
    };

    const signUrl = "http://localhost:12001/sign/create64";
    const request = getRequest();
    request.open("POST", signUrl, true);
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onreadystatechange = function () {
        console.log(request)
        if (request.readyState == 4 && request.status == 200) {
            var res = JSON.parse(request.response);
            if (res.status != "error") {
                var sKey = res.outputData;
                document.getElementById(idobj).value = sKey;
                document.getElementById(idobj).readOnly = true;
            }
        }
        else {
            if (onError) {
                onError();
            }
        }
    }
    request.send(JSON.stringify(jsonBody));
    //request.send(JSON.stringify({ "extension": extension, "fileUrl": fileUrl, "uploadUrl": uploadUrl }));
}

function OBJtoXML(obj) {
    let xml = '';
    for (const prop in obj) {
        if (obj[prop]) {
            xml += obj[prop] instanceof Array ? '' : "<" + prop + ">";
            if (obj[prop] instanceof Array) {
                for (var array in obj[prop]) {
                    xml += "<" + prop + ">";
                    xml += OBJtoXML(new Object(obj[prop][array]));
                    xml += "</" + prop + ">";
                }
            } else if (typeof obj[prop] == "object") {
                xml += OBJtoXML(new Object(obj[prop]));
            } else {
                xml += obj[prop];
            }
            xml += obj[prop] instanceof Array ? '' : "</" + prop + ">";
        }
    }
    xml = `<RegistrationConfirm>${xml.replace(/<\/?[0-9]{1,}>/g, '')}</RegistrationConfirm>`;
    return xml;
}