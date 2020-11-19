$(function () {
    $("body").on("click", ".openHangHoaChiTiet", function () {
        var iID_MaHangHoa = $(this).data("id");
        var Url = ServerUrl + "/BoPhanMotCua/Partial_HangHoaChiTiet?iID_MaHangHoa=" + iID_MaHangHoa;
        $("#HangHoaChiTiet").load(Url);
    });
});

$(function () {
    $("body").on("click", "#btnXuLyChiTieuKiemTra", function () {
        var bootstrapValidator = $("#formXuLyChiTieu").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formXuLyChiTieu")[0]);
            $.ajax({
                url: ServerUrl + '/BoPhanMotCua/XuLyChiTieuSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#XuLyChiTieu').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#XuLyChiTieu').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});