$(function () {
    $("body").on("click", ".openHangHoaChiTiet", function () {
        var iID_MaHangHoa = $(this).data("id");
        var Url = ServerUrl + "/BoPhanMotCua/Partial_HangHoaChiTiet?iID_MaHangHoa=" + iID_MaHangHoa;
        $("#HangHoaChiTiet").load(Url);
    });
});
