$(function () {
    $("body").on("click", ".opentuchoi", function () {

        var iID_MaHangHoa = $(this).data("id");
        $("#TC_iID_MaHangHoa").val(iID_MaHangHoa);
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
                url: ServerUrl + '/ChoXemXetKetQua/TuChoiSubmit',
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
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/ChoXemXetKetQua/Thoat',
            type: 'POST',
            data: { iID_MaHangHoa: iID_MaHangHoa},
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
    $("body").on("click", "#btnDongY", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/ChoXemXetKetQua/DongY',
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
    $("body").on("click", "#btnThuHoi", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/ChoXemXetKetQua/ThuHoi',
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