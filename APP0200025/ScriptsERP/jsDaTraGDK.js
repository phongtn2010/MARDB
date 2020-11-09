$(function () {
    $("body").on("click", ".openthuhoi", function () {
        var iID_MaHoSo = $(this).data("id");
        var sMaHoSo = $(this).data("mahoso");
        $("#TC_iID_MaHoSo").val(iID_MaHoSo);
        $("#mahoso").html(sMaHoSo);
    });
});

$(function () {
    $("body").on("click", "#btnTuChoi", function () {
        var bootstrapValidator = $("#formTuChoi").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            var formData = new FormData($("#formTuChoi")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: ServerUrl + '/DaTraGDK/ThuHoiSubmit',
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
                        showToast_Success();
                        location.href = response.value;
                    }
                    else {
                        showToast_Error();
                    }
                },
                error: function (response) {
                    $('#TuChoi').modal('toggle');
                    showToast_Error();
                }
            });
            return false;
        }
        else return;
    });
});