


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
        $.ajax({
            url: '/LanhDaoCuc/TuChoiHoSoSubmit',
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
            url: '/LanhDaoPhong/Thoat',
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


///Hang hoa


$(function () {
    $("body").on("click", "#btnDongY", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: '/LanhDaoCuc/DongY',
            type: 'POST',
            data: { iID_MaHangHoa: iID_MaHangHoa },
            success: function (response) {

                if (response.success) {
                    location.href = response.value;
                }
            }
        });
    });
});
$(function () {
    $("body").on("click", ".opentuchoiHH", function () {

        var iID_MaHangHoa = $(this).data("id");
        $("#TC_iID_MaHangHoa").val(iID_MaHangHoa);
      
    });
});

$(function () {
    $("body").on("click", "#btnTuChoiHH", function () {
        var formData = new FormData($("#formTuChoiHH")[0]);
        //var OrderService = $("#OrderService").val();
        $.ajax({
            url: '/LanhDaoCuc/TuChoiHoSoHHSubmit',
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
    $("body").on("click", "#btnThoatHH", function () {
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoa").val();
        $.ajax({
            url: '/LanhDaoPhong/ThoatHH',
            type: 'POST',
            data: { iID_MaHangHoa: iID_MaHangHoa },
            success: function (response) {

                if (response.success) {
                    location.href = response.value;
                }
            }
        });
    });
});

//End Hang hoa