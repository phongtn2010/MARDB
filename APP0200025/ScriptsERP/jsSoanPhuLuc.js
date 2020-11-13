$(function () {
    $("body").on("change", "#Edit_iID_MaPhanLoai", function () {
        var iID_MaPhanLoai = $(this).val();
        var iID_MaHoSo = $("#Edit_iID_MaHoSo").val();
        var url = ServerUrl + "/ChuyenVien/Partial_HangHoa?iID_MaHoSo=" + iID_MaHoSo + "&iID_MaPhanLoai=" + iID_MaPhanLoai;
        $("#divHH").load(url);      
    });
});


$(function () {
    $("body").on("click", ".openXuLyChiTieu", function () {
        var iID_MaHangHoa = $(this).data("id");
        var Url = ServerUrl + "/ChuyenVien/XuLyChiTieuKiemTra?iID_MaHangHoa=" + iID_MaHangHoa;
        $("#XuLyChiTieu").load(Url);
        //$("#XuLyChiTieu").modal("Show");
    });
});

$(function () {
    $("body").on("click", "#btnXuLyChiTieuKiemTra", function () {
        var bootstrapValidator = $("#formXuLyChiTieu").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formXuLyChiTieu")[0]);
            $.ajax({
                url: ServerUrl + '/ChuyenVien/XuLyChiTieuSubmit',
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