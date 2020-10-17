﻿$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {
        var formData = new FormData($("#formYeuCau")[0]);
        //var OrderService = $("#OrderService").val();
        $.ajax({
            url: '/ChoTiepNhanHoSo/YeuCauBoXungSubmit',
            type: 'POST',
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            enctype: 'multipart/form-data',
            processData: false,
            success: function (response) {
                $('#YeuCauBoSung').modal('toggle');
            },
            error: function (response) {
                alert("Bạn cần xử lý mẫu trước");
                $('#responsive').modal('toggle');
            }

        });
        return false;
    });
});

$(function () {
    $("body").on("click", ".openyeucaubosung", function () {

        var iID_MaHoSo = $(this).data("id");
        $("#BS_iID_MaHoSo").val(iID_MaHoSo);
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
            url: '/ChoTiepNhanHoSo/YeuCauBoXungSubmit',
            type: 'POST',
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            enctype: 'multipart/form-data',
            processData: false,
            success: function (response) {
                $('#YeuCauBoSung').modal('toggle');
            },
            error: function (response) {
                alert("Bạn cần xử lý mẫu trước");
                $('#responsive').modal('toggle');
            }

        });
        return false;
    });
});