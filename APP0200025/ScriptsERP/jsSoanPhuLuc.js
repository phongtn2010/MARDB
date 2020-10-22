$(function () {
    $("body").on("change", "#Edit_iID_MaPhanLoai", function () {
       
        var iID_MaPhanLoai = $(this).val();
        var iID_MaHoSo = $("#Edit_iID_MaHoSo").val();
        var url = "/ChuyenVien/Partial_HangHoa?iID_MaHoSo=" + iID_MaHoSo + "&iID_MaPhanLoai=" + iID_MaPhanLoai;
        $("#divHH").load(url);      

    });
});