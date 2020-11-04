$(function () {
    $("body").on("click", "#btnThuHoi", function () {    
        var formData = new FormData($("#formThuHoi")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: '/DaCapThongBaoKetQua/ThuHoiSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#responsive').modal('toggle');
                    if (response.success) {
                        location.href = response.value;
                    }
                },
                error: function (response) {
                    
                    $('#responsive').modal('toggle');
                }

            });
            return false;
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
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoa").val();
        $.ajax({
            url: '/DaCapThongBaoKetQua/Thoat',
            type: 'POST',
            data: { iID_MaHangHoa: iID_MaHangHoa},
            success: function (response) {
                
                if (response.success) {
                    location.href = response.value;
                }
            }
        });
    });
});