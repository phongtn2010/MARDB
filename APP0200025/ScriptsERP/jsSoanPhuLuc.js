$(function () {
    $("body").on("change", "#Edit_iID_MaPhanLoai", function () {
        var iID_MaPhanLoai = $(this).val();
        var iID_MaHoSo = $("#Edit_iID_MaHoSo").val();
        var url = ServerUrl + "/ChuyenVien/Partial_HangHoa?iID_MaHoSo=" + iID_MaHoSo + "&iID_MaPhanLoai=" + iID_MaPhanLoai;
        $("#divHH").load(url);      
    });
});


$(function () {
    $("body").on("click", ".openXuLyChiTieu", function () {
        var iID_MaHangHoa = $(this).data("id");
        var Url = ServerUrl + "/ChuyenVien/XuLyChiTieuKiemTra?iID_MaHangHoa=" + iID_MaHangHoa;
        //var sMaHoSo = $(this).data("mahoso");
        //$("#TC_iID_MaHoSo").val(iID_MaHoSo);
        $("#XuLyChiTieu").load(Url);
        $("#XuLyChiTieu").modal("Show");
    });
});