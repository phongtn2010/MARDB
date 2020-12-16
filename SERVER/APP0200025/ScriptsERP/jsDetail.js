var urldetail = "";
$(function () {
    $("body").on("change", "#Detail_iID_MaHangHoa", function () {
        var iID_MaHangHoa = $(this).val();
        var url = urldetail + "?iID_MaHangHoa=" + iID_MaHangHoa;
         $("#divHH").load(url);
    });
});