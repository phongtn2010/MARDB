$(function () {
    $("body").on("click", ".btnGuiDoanhNghiep", function () {
        var iID_MaHoSo = $(this).data("id");
        $.ajax({
            url: ServerUrl + '/HoSoDaTuChoiXacNhan/GuiDoanhNghiepSubmit',
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
    });
});

$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHoSo = $("#Detail_iID_MaHoSo").val();
        $.ajax({
            url: ServerUrl + '/HoSoDaTuChoiXacNhan/Thoat',
            type: 'POST',
            data: { iID_MaHoSo: iID_MaHoSo },
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