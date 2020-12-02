$(function () {
    $("body").on("click", "#btnThuHoi", function () {    
        var bootstrapValidator = $("#formThuHoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formThuHoi")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: ServerUrl + '/DaCapThongBaoKetQua/ThuHoiSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#ThuHoi').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#ThuHoi').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});

$(function () {
    $("body").on("click", ".openthuhoi", function () {
        var iID_MaHangHoa = $(this).data("id");
        $("#TH_iID_MaHangHoa").val(iID_MaHangHoa);
    });
});


$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoaThoat").val();
        $.ajax({
            url: ServerUrl + '/DaCapThongBaoKetQua/Thoat',
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