$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {   
        var bootstrapValidator = $("#formYeuCau").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formYeuCau")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: ServerUrl + '/ChoTiepNhanHoSo/YeuCauBoSungSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#YeuCauBoSung').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#YeuCauBoSung').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;        
    });
});

$(function () {
    $("body").on("click", ".openyeucaubosung", function () {
        var iID_MaHoSo = $(this).data("id");
        $("#BS_iID_MaHoSo").val(iID_MaHoSo);
    });
});

$(function () {
    $("body").on("click", ".opentuchoi", function () {

        var iID_MaHoSo = $(this).data("id");
        var sMaHoSo = $(this).data("mahoso");
        $("#TC_iID_MaHoSo").val(iID_MaHoSo);
        $("#mahoso").html(sMaHoSo);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var bootstrapValidator = $("#formTuChoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formTuChoi")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: ServerUrl + '/ChoTiepNhanHoSo/TuChoiHoSoSubmit',
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
    $("body").on("click", ".btnTiepNhan", function () {
        var iID_MaHoSo = $(this).data("id");
        $.confirm({
            title: "THÔNG BÁO XÁC NHẬN",
            text: "Bạn có chắc chắn muốn tiếp nhận hồ sơ không?",
            confirm: function (button) {               
                $.ajax({
                    url: ServerUrl + '/ChoTiepNhanHoSo/TiepNhanHoSoSubmit',
                    type: 'POST',
                    data: { iID_MaHoSo: iID_MaHoSo },
                    success: function (response) {
                        if (response.success) {
                            showToast_Success();
                            location.reload();
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
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHoSo = $("#Detail_iID_MaHoSo").val();
        $.ajax({
            url: ServerUrl + '/ChoTiepNhanHoSo/Thoat',
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

function isValidDate(s) {
    var bits = s.split('/');
    var d = new Date(bits[2] + '/' + bits[1] + '/' + bits[0]);
    return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
}

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

function checkvalidAndSubmit() {
    var tuNgay = $("#Index_viTuNgayDen").val();
    var denNgay = $("#Index_viDenNgayDen").val(); 

    var tuNgayTiepNhan = $("#Index_viTuNgayTiepNhan").val();
    var denNgayTiepNhan = $("#Index_viDenNgayTiepNhan").val();
    var d = 0;
    var arrError = [];

    if (tuNgay.length > 0 && !isValidDate(tuNgay)) {
        arrError[d] = ["Index_err_TuNgayDen", "Từ ngày không hợp lệ"];
        d = d + 1;
    }
    if (denNgay.length > 0 && !isValidDate(denNgay)) {
        arrError[d] = ["Index_err_DenNgayDen", "Đến ngày không hợp lệ"];
        d = d + 1;
    }
    if (typeof tuNgayTiepNhan !== 'undefined' && tuNgayTiepNhan.length > 0 && !isValidDate(tuNgayTiepNhan)) {
        arrError[d] = ["Index_err_TuNgayTiepNhan", "Từ ngày tiếp nhận không hợp lệ"]
        d = d + 1;
    }
    if (typeof denNgayTiepNhan !== 'undefined' && denNgayTiepNhan.length > 0 && !isValidDate(denNgayTiepNhan)) {
        arrError[d] = ["Index_err_DenNgayTiepNhan", "Đến ngày tiếp nhận không hợp lệ"]
        d = d + 1;
    }
    if (arrError.length > 0) {
        var spans = $(".field-validation-valid");

        for (var i = 0; i < spans.length; i++) {
            var span = spans[i];
            var errorName = $(span).attr("data-valmsg-for");
            for (var j = 0; j < arrError.length; j++) {
                if (errorName === arrError[j][0]) {
                    $(span).removeClass("field-validation-valid")
                    $(span).addClass("field-validation-error")
                    $(span).html(arrError[j][1]);

                }
            }

        }
    }
    else {
        $("#formSearch").submit();
    }
}