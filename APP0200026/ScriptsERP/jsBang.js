/* Ba

*/

var browserID = 'mozilla';//ie = 'msie'
var urlServerPath = '/';
function DetectBrowser()
{
    jQuery.each(jQuery.browser, function (i, val) {
        if (val == true) {
            browserID = i.toString();
        }
    });
}


var Bang_keys;

var Bang_Scroll_Size = 17; //Chiều rộng của cột tiêu đề
var Bang_ID = "BangDuLieu";
var Bang_ID_Div = ""; //DIV bao ngoài của cả bảng
var Bang_ID_TB00 = "";
var Bang_ID_TB00_Div = "";
var Bang_ID_TB01 = "";
var Bang_ID_TB01_Div = "";
var Bang_ID_TB10 = "";
var Bang_ID_TB10_Div = "";
var Bang_ID_TB11 = "";
var Bang_ID_TB11_Div = "";
var Bang_ID_TR_BangDuLieu = "";
var BangDuLieuID_Slide = "";
var BangDuLieuID_Fixed = "";
var BangDuLieuID_Slide_Div = "";

/* Bang_Url_getGiaTri: url cua ham lay gia tri sau khi nhap xong o Autocomplete*/
var Bang_Url_getGiaTri = "";
/* Bang_Url_getGiaTri: url cua ham lay gia tri ngay khi bam 1 phim tren o Autocomplete*/
var Bang_Url_getDanhSach = "";

function Bang_adjustTable(okInit) {
    //Hiệu chỉnh lại các bảng con khi khung thay đổi
    var w0 = fnGetWidthById(Bang_ID_Div);
    var h0 = fnGetHeightById(Bang_ID_Div);
    var Bang_FixedRow_Height = fnGetHeightById(Bang_ID_TB01); //Chiều cao của phần hàng tiêu đề
    var Bang_FixedCol_Width = fnGetWidthById(Bang_ID_TB00); //Chiều rộng của cột tiêu đề
    $('#' + Bang_ID_TB11_Div).css('width', w0 - 4 - Bang_FixedCol_Width);
    $('#' + Bang_ID_TB11_Div).css('height', h0 - 4 - Bang_FixedRow_Height);
    $('#' + Bang_ID_TB01_Div).css('width', w0 - 4 - Bang_FixedCol_Width - Bang_Scroll_Size);
    $('#' + Bang_ID_TB10_Div).css('height', h0 - 4 - Bang_FixedRow_Height - Bang_Scroll_Size);
    //alert(w0 +',' + Bang_FixedCol_Width + ','+(w0 - 4 - Bang_FixedCol_Width));
}

function Bang_GanGiaTriCacBienTongThe() {
    Bang_ID_Div = Bang_ID + "_div";
    Bang_ID_TB00 = Bang_ID + "_TB00";
    Bang_ID_TB00_Div = Bang_ID_TB00 + "_div";
    Bang_ID_TB01 = Bang_ID + "_TB01";
    Bang_ID_TB01_Div = Bang_ID_TB01 + "_div";
    Bang_ID_TB10 = Bang_ID + "_TB10";
    Bang_ID_TB10_Div = Bang_ID_TB10 + "_div";
    Bang_ID_TB11 = Bang_ID + "_TB11";
    Bang_ID_TB11_Div = Bang_ID_TB11 + "_div";
    Bang_ID_TR_BangDuLieu = Bang_ID + "_TR_DuLieu";
    BangDuLieuID_Slide = Bang_ID_TB11;
    BangDuLieuID_Fixed = Bang_ID_TB10;
    BangDuLieuID_Slide_Div = Bang_ID_TB11_Div;
}

$(document).ready(function () {
    Bang_GanGiaTriCacBienTongThe();

    Bang_KhoiTao();
    Bang_HienThiDuLieu();

    Bang_keys = new KeyTable({
        "table": document.getElementById(BangDuLieuID_Slide)
    });


    Bang_adjustTable(true);
    $('#' + BangDuLieuID_Slide_Div).scrollLeft(0);
    $('#' + BangDuLieuID_Slide_Div).scrollTop(0);

    if (Bang_ChiDoc == false) {
        Bang_Ready();
        //Bang_keys.fnSetFocusFirstEditCell();
    }
    if (Bang_nH > 0 && Bang_nC > 0) {
        Bang_keys.fnSetFocus(0, 0);
    }

    var fn = window[Bang_ID + '_onLoad'];
    if (typeof fn == 'function') {
        fn();
    }
});

$(window).resize(function () {
    Bang_adjustTable(false);
});

function Bang_onFocus(h, c) {
    var TenHam = Bang_ID + '_onCellFocus';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        fn(h, c);
    }
}

function Bang_onBeforeFocus(h, c) {
    var TenHam = Bang_ID + '_onCellBeforeFocus';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        return fn(h, c);
    }
    return true;
}

function Bang_onKeypress_F(strKeyEvent) {
    var h = Bang_keys.Row();
    var c = Bang_keys.Col() - Bang_nC_Fixed;
    var TenHam = Bang_ID + '_onKeypress_' + strKeyEvent;
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        if (fn(h, c) == false) {
            return false;
        }
    }
    if (strKeyEvent == "F10") {
        Bang_HamTruocKhiKetThuc();
    }
}

function Bang_onDblClick(h, c) {
    var TenHam = Bang_ID + '_onDblClick';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        if (fn(h, c) == false) {
            return false;
        }
    }
    if(Bang_arrType[c]==2) {
        //Neu o khong duoc nhap thi bo qua
        if (Bang_arrEdit[h][c] == false) {
            return false;
        }

        //Goi ham BeforeEdit neu co, Neu ham tra lai gia tri la FALSE thi bo qua
        var fnBeforeEdit = window['Bang_onCellBeforeEdit'];
        if (typeof fnBeforeEdit == 'function') {
            if (fnBeforeEdit(h, c) == false) {
                return false;
            }
        }

        var checkbox_value = Bang_arrGiaTri[h][c];
        if (checkbox_value == "1") {
            checkbox_value = "0";
        }
        else {
            checkbox_value = "1";
        }
        Bang_GanGiaTriThatChoO(h, c, checkbox_value);
        var checkbox_fn = window[Bang_ID + '_onCellAfterEdit'];
        if (typeof checkbox_fn == 'function') {
            checkbox_fn(h, c);
        }
    }
    return true;
}

//Ham chi dinh JSON lay du lieu
var Bang_curH = -1;
var Bang_curC = -1;
function Bang_txtONhapDuLieu_Autocomplete_onSource(txt, request, response) {
    var TenHam = Bang_ID + '_txtONhapDuLieu_Autocomplete_onSource';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        return fn(txt, request, response);
    }
    var posCurr = Bang_keys.fnGetCurrentPosition();
    var h = posCurr[1];
    var c = posCurr[0] + Bang_nC_Fixed;
    if (h != null && c != null) {
        Bang_curH = h;
        Bang_curC = c;
        var arrTruong = Bang_LayTenTruongVaTruongGan(c);
        var Truong = arrTruong[0], TruongGan = arrTruong[1];

        var url = Bang_Url_getDanhSach;
        var sDSGiaTri="";
        if (Truong == "sDonGia")
        {
            sDSGiaTri = DH_DonHang_iID_MaSanPham + "," + DH_DonHang_iID_MaKhachHang;           
        }
        else if (Truong == "dNgayXacNhanGiao")
        {
            sDSGiaTri = Bang_GhepGiaTriCuaTruong("dNgayXacNhanGiao", "iID_MaDonHang", "iID_MaSanPham");
        }
        else if (Truong == "sGiaXuat") {
            sDSGiaTri = Bang_GhepGiaTriCuaTruong("sGiaXuat", "iID_MaDonHang", "iID_MaSanPham", "dNgayXacNhanGiao");
        }
        $.getJSON(url, { Truong: Truong, GiaTri: request.term, DSGiaTri: sDSGiaTri }, response);
    }
}

function Bang_txtONhapDuLieu_Autocomplete_onSelect(txt, event, ui) {
    var TenHam = Bang_ID + '_txtONhapDuLieu_Autocomplete_onSelect';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        return fn(txt, event, ui);
    }
    return null;
}

function Bang_txtONhapDuLieu_Autocomplete_renderItem(txt, ul, item) {
    var TenHam = Bang_ID + '_txtONhapDuLieu_Autocomplete_renderItem';
    var fn = window[TenHam];
    if (typeof fn == 'function') {
        return fn(txt, ul, item);
    }
    var v = $(txt).val();
    var i;
    var text = item.label;
    for (i = text.length - v.length; i >= 0; i--) {
        if (v.toUpperCase() == text.substr(i, v.length).toUpperCase()) {
            text = text.substr(0, i) + '<b>' + text.substr(i, v.length) + '</b>' + text.substr(i + v.length);
        }
    }
    return $('<li></li>')
                .data('item.autocomplete', item)
                .append('<a>' + text + '</a>')
                .appendTo(ul);
}

//Hàm lấy tên trường và tên trường gán của cột 'c' trong bảng dữ liệu
function Bang_LayTenTruongVaTruongGan(c) {
    var Truong = "", TruongGan = "";
    //Xác định tên trường tìm dữ liệu
    if (Bang_arrMaCot[c].startsWith("sProjectCode")) {
        Truong = 'sProjectCode';
        TruongGan = 'iID_MaDuAn,sProjectName';
    }
    else if (Bang_arrMaCot[c].startsWith("sProjectName")) {
        Truong = 'sProjectName';
        TruongGan = 'iID_MaDuAn,sProjectCode';
    }
    else if (Bang_arrMaCot[c].startsWith("sTieuDeLoaiVanBan")) {
        Truong = 'sTieuDeLoaiVanBan';
        TruongGan = 'iID_MaLoaiVanBan,iThoiGianLoaiVanBan';
    }
    else if (Bang_arrMaCot[c].startsWith("sUserName")) {
        Truong = 'sUserName';
        TruongGan = 'sID_MaNguoiDung';
    }
    else if (Bang_arrMaCot[c].startsWith("sLanhDao")) {
        Truong = 'sLanhDao';
        TruongGan = 'sID_MaLanhDao';
    }
    else if (Bang_arrMaCot[c].startsWith("sMaKhachHang")) {
        Truong = 'sMaKhachHang';
        TruongGan = 'iID_MaKhachHang';
    }
    else if (Bang_arrMaCot[c].startsWith("sSoHopDong")) {
        Truong = 'sSoHopDong';
        TruongGan = 'iID_MaHopDong';
    }
    else if (Bang_arrMaCot[c].startsWith("sTenTram")) {
        Truong = 'sTenTram';
        TruongGan = 'iID_MaNhaTram,sDiaChiTram';
    }

    return [Truong, TruongGan];
}

//Hàm ghép mã của một trường
//sTruongTen là trường nhập trường mã là trường cần ghép
function Bang_GhepGiaTriCuaTruong(sTruongTen, sTruongMa) {
    var vR = "";
    var cs;
    var h = Bang_keys.Row();
    var c = Bang_keys.Col();    
    if (Bang_arrMaCot[c] == sTruongTen) {
        cs = Bang_arrCSMaCot[sTruongMa];
        vR = Bang_arrGiaTri[h][cs];        
    }
    return vR;
}

function Bang_GhepGiaTriCuaTruong(sTruongTen, sTruongMa1, sTruongMa2) {
    var vR = "";
    var cs;
    var h = Bang_keys.Row();
    var c = Bang_keys.Col();
    if (Bang_arrMaCot[c] == sTruongTen) {
        cs = Bang_arrCSMaCot[sTruongMa1];
        vR = Bang_arrGiaTri[h][cs];
        cs = Bang_arrCSMaCot[sTruongMa2];
        vR =vR+","+ Bang_arrGiaTri[h][cs];
    }
    return vR;
}

function Bang_GhepGiaTriCuaTruong(sTruongTen, sTruongMa1, sTruongMa2, sTruongMa3) {
    var vR = "";
    var cs;
    var h = Bang_keys.Row();
    var c = Bang_keys.Col();
    if (Bang_arrMaCot[c] == sTruongTen) {
        cs = Bang_arrCSMaCot[sTruongMa1];
        vR = Bang_arrGiaTri[h][cs];

        cs = Bang_arrCSMaCot[sTruongMa2];
        vR = vR + "," + Bang_arrGiaTri[h][cs];

        cs = Bang_arrCSMaCot[sTruongMa3];
        vR = vR + "," + Bang_arrGiaTri[h][cs];
    }
    return vR;
}

/* Su kien Bang_onCellBeforeEdit
*   - Muc dinh: Su kien xuat hien truoc khi nhap du lieu moi tren o (h,c) cua bang du lieu
*   - Dau vao:  + h: chi so hang 
*               + c: chi so cot
*/
function Bang_onCellBeforeEdit(h, c) {
    //Goi ham BeforeEdit neu co, Neu ham tra lai gia tri la FALSE thi bo qua
    var fnBeforeEdit = window[Bang_ID + '_onCellBeforeEdit'];
    if (typeof fnBeforeEdit == 'function') {
        return fnBeforeEdit(h, c);
    }
    return true;
}

/* Su kien Bang_onCellAfterEdit
*   - Muc dinh: Su kien xuat hien sau khi nhap du lieu moi tren o (h,c) cua bang du lieu
*   - Dau vao:  + h: chi so hang 
*               + c: chi so cot
*/
function Bang_onCellAfterEdit(h, c) {
    var fnCellAfterEdit = window[Bang_ID + '_onCellAfterEdit'];

    if (Bang_arrType[c] == 3) {
        var arrTruong = Bang_LayTenTruongVaTruongGan(c);
        var Truong = arrTruong[0], TruongGan = arrTruong[1];
        var url = Bang_Url_getGiaTri;
       // var GiaTriDau = Bang_arrGiaTri[h][c].split(',')[0];
        //var index = GiaTriDau.indexOf('-', 7);
        //var GiaTri = Bang_arrGiaTri[h][c].split('-')[0];
        //if (index > 0) GiaTri = GiaTriDau;
        var GiaTri = Bang_arrGiaTri[h][c];
        GiaTri = $.trim(GiaTri);

        url += '?Truong=' + Truong;
        url += '&GiaTri=' + GiaTri;
//        if(Truong == "dNgayXacNhanGiao")
//        {
//            url += '&DSGiaTri=' + Bang_GhepGiaTriCuaTruong("dNgayXacNhanGiao", "iID_MaDonHang", "iID_MaSanPham");
//        }
        url = unescape(url);


        $.getJSON(url, function (item) {
            var csGan;
            
            if (item != null) {
                //Gán trường hiển thị
                Bang_GanGiaTriThatChoO(h, c, item.label);
                if (Truong == "dNgayXacNhanGiao")
                {
                    //Gán trường ẩn
                    //TruongGan = 'iSLCanGiao,iSLKeHoach';
                    csGan = Bang_arrCSMaCot[TruongGan.split(',')[0]];
                    Bang_GanGiaTriThatChoO(h, csGan, item.SoLuongCanGiao);

                    csGan = Bang_arrCSMaCot[TruongGan.split(',')[1]];
                    Bang_GanGiaTriThatChoO(h, csGan, item.SoLuongKeHoach);
                }
                else {
                    //Gán trường ẩn
                    var arr = TruongGan.split(',');
                    if (arr.length > 1)
                    {
                        var i = 0;
                        var TenTruong = '';
                        for (i = 0; i < arr.length; i++) {
                            TenTruong = arr[i];                            
                            csGan = Bang_arrCSMaCot[TenTruong];
                            if (csGan >=0)
                            {
                                for (var obj in item) {
                                    if (obj == TenTruong) {
                                        Bang_GanGiaTriThatChoO(h, csGan, item[obj]);
                                        break;
                                    }
                                }
                            }
                            
                            
                        }
                    }
                    else
                    {
                        csGan = Bang_arrCSMaCot[TruongGan];
                        Bang_GanGiaTriThatChoO(h, csGan, item.value);
                    }
                    
                }
                
                
            }
            else {
                //Gán trường hiển thị
                Bang_GanGiaTriThatChoO(h, c, "");
                //Gán trường ẩn
                csGan = Bang_arrCSMaCot[TruongGan];
                Bang_GanGiaTriThatChoO(h, csGan, "");
            }
            if (typeof fnCellAfterEdit == 'function') {
                return fnCellAfterEdit(h, c, item);
            }
        });
    }
    else {
        if (typeof fnCellAfterEdit == 'function') {
            return fnCellAfterEdit(h, c);
        }
    }
    return true;
}


var Bang_fnCellValueChanged_CoHam = false;
var Bang_fnCellValueChanged = null;
function Bang_onCellValueChanged(h, c, GiaTriCu) {
    //Đổi màu khi đồng ý, từ chối nếu có
    if (Bang_arrMaCot[c] == "bDongY" || Bang_arrMaCot[c] == "sLyDo") {
        var bDongY = Bang_LayGiaTri(h, "bDongY");
        var sLyDo = Bang_LayGiaTri(h, "sLyDo");
        var sMauSac = Bang_sMauSac_ChuaDuyet;
        if (bDongY == "1") {
            sMauSac = Bang_sMauSac_DongY;
        }
        else {
            if (sLyDo != "") {
                sMauSac = Bang_sMauSac_TuChoi;
            }
        }
        Bang_GanGiaTriThatChoO_colName(h, "sMauSac", sMauSac);
    }
    //Gọi hàm CellValueChanged
    if (Bang_fnCellValueChanged_CoHam == false) {
        Bang_fnCellValueChanged = window[Bang_ID + '_onCellValueChanged'];
        if (typeof Bang_fnCellValueChanged == 'function') {
            Bang_fnCellValueChanged_CoHam = true;
        }
    }
    if (Bang_fnCellValueChanged_CoHam) {
        Bang_fnCellValueChanged(h, c, GiaTriCu);
    }
}

function Bang_onCellKeyUp(h, c, e, iKey) {
    var fnKeyUp = window[Bang_ID + '_onCellKeyUp'];
    if (typeof fnKeyUp == 'function') {
        return fnKeyUp(h, c, e, iKey);
    }
    return true;
}

function Bang_onEnter_NotSetCellFocus() {
    var fn = window[Bang_ID + '_onEnter_NotSetCellFocus'];
    if (typeof fn == 'function') {
        return fn();
    }
    return true;
}

Bang_fnScroll = function () {
    //Hiệu chỉnh bảng hàng tiêu đề theo bảng dữ liệu
    $('#' + Bang_ID_TB01_Div).scrollLeft($('#' + BangDuLieuID_Slide_Div).scrollLeft());
    //Hiệu chỉnh bảng cột tiêu đề theo bảng dữ liệu
    var yTop = $('#' + BangDuLieuID_Slide_Div).scrollTop();
    $('#' + Bang_ID_TB10_Div).scrollTop(yTop);
    Bang_SetPosition(yTop);
}