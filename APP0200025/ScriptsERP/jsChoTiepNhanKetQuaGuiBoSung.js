$(function () {
    $("body").on("click", "#btnBoSungYeuCau", function () {    
        var formData = new FormData($("#formYeuCau")[0]);
            //var OrderService = $("#OrderService").val();
            $.ajax({
                url: '/ChoTiepNhanKetQuaGuiBoSung/YeuCauBoSungSubmit',
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
            url: '/ChoTiepNhanKetQuaGuiBoSung/TuChoiHoSoSubmit',
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
        var iID_MaHangHoa = $("#Detail_iID_MaHangHoa").val();
        $.ajax({
            url: '/ChoTiepNhanKetQuaGuiBoSung/Thoat',
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