$(function () {
    $("body").on("click", ".btnGuiDoanhNghiep", function () {
        var iID_MaHoSo = $(this).data("id");
        $.confirm({
            title: "THÔNG BÁO XÁC NHẬN",
            text: "Bạn có chắc chắn muốn gửi doanh nghiệp không?",
            confirm: function (button) {
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