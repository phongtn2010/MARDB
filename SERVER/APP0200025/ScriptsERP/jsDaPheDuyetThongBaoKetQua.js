$(function () {
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoaThoat").val();
        $.ajax({
            url: ServerUrl + '/DaPheDuyetThongBaoKetQua/Thoat',
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