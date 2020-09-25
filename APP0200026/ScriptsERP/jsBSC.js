var ServerURL = "";
var jsMyip_Client = "";
function BangDuLieu_onLoad() {
    if (Bang_nH == 0) {
        Bang_ThemHang();
    }
    Bang_keys.fnSetFocus(0, 0);
}

function BangDuLieu_onKeypress_F2(h, c) {
    var hn = Bang_nH;
    Bang_ThemHang(hn);
    Bang_keys.fnSetFocus(hn, 0);
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
    if (Bang_arrMaCot[c] == "sVBDen_NgayNhan") {
        Bang_arrThayDoi[h][0] = true;
    }
    if (Bang_arrMaCot[c] == "sNgayTrinh") {
        Bang_arrThayDoi[h][0] = true;
    }
    if (Bang_arrMaCot[c] == "iDanhGia_SoNgayThem") {
        Bang_arrThayDoi[h][0] = true;
    }
    if (Bang_arrMaCot[c] == "sTieuDeLoaiVanBan") {
        Bang_arrThayDoi[h][0] = true;
    }
    if (Bang_arrMaCot[c] == "iDanhGia_SoNgayThem") {
        Bang_arrThayDoi[h][0] = true;
    }
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
    if (Bang_arrMaCot[c] == "sVBDen_NgayNhan") {
        var dNgayTrinh = Bang_arrGiaTri[h][Bang_arrCSMaCot["sNgayTrinh"]];
        var dNgayNhan = Bang_arrGiaTri[h][c];


        var arrNgayTrinh = new Array();
        arrNgayTrinh = dNgayTrinh.split('/');

        var arrNgayNhan = new Array();
        arrNgayNhan = dNgayNhan.split('/');

        var dateNgayTrinh = new Date(arrNgayTrinh[2], arrNgayTrinh[1], arrNgayTrinh[0]);
        var dateNgayNhan = new Date(arrNgayNhan[2], arrNgayNhan[1], arrNgayNhan[0]);
        var idate = dateNgayTrinh.getTime() - dateNgayNhan.getTime();
        
        var iSoNgay = idate / (1000 * 3600 * 24);
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["iDanhGia_SoNgayThucTe"], iSoNgay);
    }
    if (Bang_arrMaCot[c] == "sNgayTrinh") {
        var dNgayTrinh = Bang_arrGiaTri[h][c];
        var dNgayNhan = Bang_arrGiaTri[h][Bang_arrCSMaCot["sVBDen_NgayNhan"]];


        var arrNgayTrinh = new Array();
        arrNgayTrinh = dNgayTrinh.split('/');

        var arrNgayNhan = new Array();
        arrNgayNhan = dNgayNhan.split('/');

        var dateNgayTrinh = new Date(arrNgayTrinh[2], arrNgayTrinh[1], arrNgayTrinh[0]);
        var dateNgayNhan = new Date(arrNgayNhan[2], arrNgayNhan[1], arrNgayNhan[0]);
        var idate = dateNgayTrinh.getTime() - dateNgayNhan.getTime();

        var iSoNgay = idate / (1000 * 3600 * 24);
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["iDanhGia_SoNgayThucTe"], iSoNgay);
    }
    if (Bang_arrMaCot[c] == "sTieuDeLoaiVanBan") {
        var iSoNgayQuyDinh = Bang_arrGiaTri[h][Bang_arrCSMaCot["iThoiGianLoaiVanBan"]];
        var iSoNgayThucTe = Bang_arrGiaTri[h][Bang_arrCSMaCot["iDanhGia_SoNgayThucTe"]];
        var iSoNgayThem = Bang_arrGiaTri[h][Bang_arrCSMaCot["iDanhGia_SoNgayThem"]];

        var iDanhGia = 0;
        if (iSoNgayThucTe > 0) {
            iDanhGia = iSoNgayQuyDinh - iSoNgayThucTe + iSoNgayThem;
        }
        else {
            iDanhGia = iSoNgayQuyDinh + iSoNgayThucTe + iSoNgayThem;
        }
          
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["iDanhGia_ThoiGianSoVoiYeuCau"], iDanhGia);

        var sDanhGia = '';
        if (iDanhGia >= 0) {
            sDanhGia = "Đạt";
        }
        else {
            sDanhGia = "Không đạt";
        }
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["sDanhGia_KetLuat"], sDanhGia);
    }
    if (Bang_arrMaCot[c] == "iDanhGia_SoNgayThem") {
        var iSoNgayQuyDinh = Bang_arrGiaTri[h][Bang_arrCSMaCot["iThoiGianLoaiVanBan"]];
        var iSoNgayThucTe = Bang_arrGiaTri[h][Bang_arrCSMaCot["iDanhGia_SoNgayThucTe"]];
        var iSoNgayThem = Bang_arrGiaTri[h][c];

        var iDanhGia = 0;
        if (iSoNgayThucTe > 0) {
            iDanhGia = iSoNgayQuyDinh - iSoNgayThucTe + iSoNgayThem;
        }
        else {
            iDanhGia = iSoNgayQuyDinh + iSoNgayThucTe + iSoNgayThem;
        }
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["iDanhGia_ThoiGianSoVoiYeuCau"], iDanhGia);

        if (iDanhGia >= 0) {
            sDanhGia = "Đạt";
        }
        else {
            sDanhGia = "Không đạt";
        }
        Bang_GanGiaTriThatChoO(h, Bang_arrCSMaCot["sDanhGia_KetLuat"], sDanhGia);
    }
}