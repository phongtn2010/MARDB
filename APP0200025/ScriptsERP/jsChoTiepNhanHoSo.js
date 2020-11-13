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