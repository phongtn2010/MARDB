$(function () {
    $("body").on("click", ".openyeucaubosung", function () {

        var iID_MaHangHoa = $(this).data("id");
        $("#BS_iID_MaHangHoa").val(iID_MaHangHoa);
    });
});

$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {    
        var bootstrapValidator = $("#formYeuCau").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formYeuCau")[0]);
            $.ajax({
                url: ServerUrl + '/ChoXuLyKetQua/YeuCauBoSungSubmit',
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

//$(function () {
//    $("body").on("click", ".openthuhoi", function () {

//        var iID_MaHangHoa = $(this).data("id");
//        $("#BS_iID_MaHangHoa").val(iID_MaHangHoa);
//    });
//});
$(function () {
    $("body").on("click", "#btnThuHoi", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/ChoXuLyKetQua/ThuHoi',
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

$(function () {
    $("body").on("click", "#btnTrinhLanhDao", function () {
        var iID_MaHangHoa = $("#Edit_iID_MaHangHoa").val();
        $.ajax({
            url: ServerUrl + '/ChoXuLyKetQua/TrinhLanhDao',
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

$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/ChoXuLyKetQua/Thoat',
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