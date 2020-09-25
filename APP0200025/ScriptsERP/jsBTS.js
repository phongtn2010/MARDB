var ServerURL = "";
var jsMyip_Client = "";
function BangDuLieu_onLoad() {
    if (Bang_nH == 0) {
        Bang_ThemHang();
    }
    Bang_keys.fnSetFocus(0, 0);
}

function BangDuLieu_onKeypress_F2(h, c) {
    //    var hn = Bang_nH;
    //    Bang_ThemHang(hn);
    //    Bang_keys.fnSetFocus(hn, 0);
    BangDuLieu_ThemHangMoi(h + 1, h);
}

function BangDuLieu_onKeypress_Delete(h, c) {
    if (Bang_ChiDoc == false && h != null) {
        Bang_XoaHang(h);
        if (h < Bang_nH) {
            Bang_keys.fnSetFocus(h, c);
        }
    }
}

function BangDuLieu_onCellAfterEdit(h, c) {
    BangDuLieu_TinhOConLai(h, c);
    if (Bang_arrMaCot[c] == "rTongTienTruocVAT") {
        Bang_arrThayDoi[h][0] = true;
    }
    if (Bang_arrMaCot[c] == "rChiPhiTruocNamTaiChinh") {
        Bang_arrThayDoi[h][0] = true;
    }
}

/* Ham BangDuLieu_ThemHangMoi
*   - Muc dinh: Tao 1 hang moi o tai vi tri 'h' lay du lieu tai vi tri 'hGiaTri'
*   - Dau vao:  + h: vi tri them
*               + hGiaTri: vi tri hang lay du lieu, =null: them 1 hang trong
*/
function BangDuLieu_ThemHangMoi(h, hGiaTri) {
    var csH = 0;
    if (h != null) {
        //Thêm 1 hàng mới vào hàng h
        csH = h;
    }
    else {
        //Thêm 1 hàng mới vào cuối bảng
        csH = Bang_nH;
    }
    Bang_ThemHang(csH);
    //Sua MaHang="": Day la hang them moi
    Bang_arrMaHang[csH] = "";
    Bang_keys.fnSetFocus(csH, 0);
}

function BangDuLieu_TinhOConLai(h, c) {
    //if (Bang_arrMaCot[c] == "sMaSanPham") {
    //    jQuery.ajaxSetup({ cache: false });
    //    var iID_MaSanPham = Bang_arrGiaTri[h][Bang_arrCSMaCot["iID_MaSanPham"]];
    //    var url = unescape(ServerURL + "/Public/Get_DonGia?term=" + iID_MaSanPham);
    //    $.getJSON(url, function (item) {
    //        if (item != null) {
    //            Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rDonGia"], item._DonGia);
    //            Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rDonGiaVND"], item._DonGiaVND);
    //        }
    //    });
    //}
    if (Bang_arrMaCot[c] == "rTongTienTruocVAT") {
        var rTongTienTruocVAT = Bang_arrGiaTri[h][c];
        var rThueVAT = rTongTienTruocVAT * 10 / 100;
        var rTongTienThanhToan = rTongTienTruocVAT + rThueVAT;
        var rChiPhiTruocNamTaiChinh = Bang_arrGiaTri[h][Bang_arrCSMaCot["rChiPhiTruocNamTaiChinh"]];

        var rChiPhiTrongNam2 = rTongTienTruocVAT - rChiPhiTruocNamTaiChinh - rTongTienTruocVAT;

        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rThueVAT"], rThueVAT);
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rTongTienThanhToan"], rTongTienThanhToan);
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rChiPhiTrongNam1"], rTongTienTruocVAT);
        //Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rChiPhiTrongNam2"], rChiPhiTruocNamTaiChinh);
    }
    if (Bang_arrMaCot[c] == "rChiPhiTruocNamTaiChinh") {
        var rChiPhiTruocNamTaiChinh = Bang_arrGiaTri[h][c];

        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["rChiPhiTrongNam2"], rChiPhiTruocNamTaiChinh);
    }
}