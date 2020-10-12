$(function () {
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
                  //  $('#' + OrderService).text("Link");
                   // $('#' + OrderService).attr("href", response);
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