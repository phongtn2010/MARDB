var GetDsHangHoa = function (iID_MaHoSo, sMaHoSo, sSoTiepNhan) {
   
    $("#HoangHoa_sMaHoSo").val(sMaHoSo);
    $("#HoangHoa_sSoTiepNhan").val(sSoTiepNhan);
    $.ajax({
        url: "/ToChucChiDinh/GetHoangHoa",
        type: "POST",
        data: { "iID_MaHoSo": iID_MaHoSo },
        success: function (result) {
            $("#tableHienThiHoangHoa").html("");
            var object = JSON.parse(result.DataTable);
            var htmlTable = "";
            for (var i = 0; i < object.length; i++) {
                htmlTable += "<tr>";
                htmlTable += "<td>" + (i + 1) + "</td>";
                htmlTable += "<td>" + object[i].sTenHangHoa + "</td>";
                htmlTable += "<td>" + (object[i].rSoLuong != null ? object[i].rSoLuong : '') + "</td>";
                htmlTable += "<td>" + (object[i].rKhoiLuong != null ? object[i].rKhoiLuong : '') + "</td>";
                htmlTable += "<td>?</td>";
                htmlTable += "<td style='text-align:center;'><a data-toggle='modal' href='#UploadKetQua'><span class='badge bg-blue'> <i class='fa fa-pencil'></i></span></a></td>";
                htmlTable += "<td style='text-align:center;'><span class='badge bg-blue'><i class='fa fa-fw fa-paper-plane-o'></i></span></td>";
                htmlTable += "<td style='text-align:center;'><span class='badge bg-blue'><i class='fa fa-fw fa-eye fa-lg'></i></span></td>";
                htmlTable += "</tr>";
            }
            $("#tableHienThiHoangHoa").append(htmlTable);
        }
    });
}


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