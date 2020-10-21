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

        var iID_MaHoSo = $(this).data("id");
        $("#TH_iID_MaHoSo").val(iID_MaHoSo);
    });
});


$(function () {
    $("body").on("click", ".opentuchoi", function () {

        var iID_MaHoSo = $(this).data("id");
        var sMaHoSo = $(this).data("mahoso");
        $("#TC_iID_MaHoSo").val(iID_MaHoSo);
        $("#mahoso").html(sMaHoSo);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var formData = new FormData($("#formTuChoi")[0]);
        //var OrderService = $("#OrderService").val();
        $.ajax({
            url: '/DaCapThongBaoKetQua/TuChoiHoSoSubmit',
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
    $("body").on("click", "#btnThoat", function () {
        var iID_MaHoSo = $("#Detail_iID_MaHoSo").val();
        $.ajax({
            url: '/DaCapThongBaoKetQua/Thoat',
            type: 'POST',
            data: { iID_MaHoSo: iID_MaHoSo},
            success: function (response) {
                
                if (response.success) {
                    location.href = response.value;
                }
            }
        });
    });
});