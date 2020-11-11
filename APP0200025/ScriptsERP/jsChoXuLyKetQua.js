$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {    
        var bootstrapValidator = $("#formYeuCau").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formYeuCau")[0]);
            $.ajax({
                url: ServerUrl + '/ChoXuLyKetQua/YeuCauBoSungSubmit',
                type: 'POST',
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (response) {
                    $('#YeuCauBoSung').modal('toggle');
                    if (response.success) {
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#YeuCauBoSung').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});

$(function () {
    $("body").on("click", ".openyeucaubosung", function () {

        var iID_MaHangHoa = $(this).data("id");
        $("#BS_iID_MaHangHoa").val(iID_MaHangHoa);
    });
});


$(function () {
    $("body").on("click", ".opentuchoi", function () {

        var iID_MaHangHoa = $(this).data("id");
        var sMaHoSo = $(this).data("mahoso");
        $("#TC_iID_MaHangHoa").val(iID_MaHangHoa);
        $("#mahoso").html(sMaHoSo);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var formData = new FormData($("#formTuChoi")[0]);
        //var OrderService = $("#OrderService").val();
        $.ajax({
            url: '/ChoXuLyKetQua/TuChoiHoSoSubmit',
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
            url: '/ChoXuLyKetQua/Thoat',
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


$(function () {
    $("body").on("click", "#btnTrinhLanhDao", function () {
        var iID_MaHangHoa = $("#Edit_iID_MaHangHoa").val();
        $.ajax({
            url: '/ChoXuLyKetQua/TrinhLanhDao',
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
    $("body").on("click", "#btnThuHoi", function () {
        var iID_MaHangHoa = $(this).data("id");
        $.ajax({
            url: '/ChoXuLyKetQua/ThuHoi',
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