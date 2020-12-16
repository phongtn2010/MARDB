$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {
        var bootstrapValidator = $("#formTuChoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formYeuCau")[0]);
            $.ajax({
                url: ServerUrl + '/ChuyenVien/YeuCauBoSungSubmit',
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
        $("#TC_iID_MaHoSo").val(iID_MaHoSo);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var bootstrapValidator = $("#formTuChoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formTuChoi")[0]);
            $.ajax({
                url: ServerUrl + '/ChuyenVien/TuChoiSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#TuChoi').modal('toggle');
                },
                error: function (response) {
                    alert("Chưa thành công");
                    $('#responsive').modal('toggle');
                }

            });
            return false;
        }
        else return;
    });
});

$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoa").val();
        $.ajax({
            url: ServerUrl + '/ChuyenVien/Thoat',
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