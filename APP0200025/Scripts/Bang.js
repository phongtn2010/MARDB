var BangDuLieu_h0 = 4, BangDuLieu_c0 = 5;
var BangDuLieu_nH = 0, BangDuLieu_nC = 0;
var arrMaHang, arrMaCot;
var arrKieuDuLieu; //0:Kieu xau; 1: Kieu so; 2:;3: Textarea; 4:datetime; 5:date; 6:month;
var arrCSMaHang, arrCSMaCot;
var arrGiaTri;
var Bang_DauCachHang = "**", Bang_DauCachO = "##";
var iChrome = 0;
var objBangDuLieu;

function Bang_KhoiTao() {
    var i, j;
    objBangDuLieu = $("#BangDuLieu")[0];
    arrMaHang = document.getElementById('idXauMaCacHang').value.split(",");
    arrMaCot = document.getElementById('idXauMaCacCot').value.split(",");
    arrKieuDuLieu = Bang_LayMangGiaTri('idXauKieuDuLieu');
    arrGiaTri = Bang_LayMangGiaTri('idXauGiaTriChiTiet');

    //Tạo mảng CSMaHang, CSMaCot
    arrCSMaHang = new Array();
    arrCSMaCot = new Array();
    for (i = 0; i < arrMaHang.length; i++) {
        arrCSMaHang[arrMaHang[i]] = i;
    }
    for (i = 0; i < arrMaCot.length; i++) {
        arrCSMaCot[arrMaCot[i]] = i;
    }

    //Sửa lại mảng giá trị
    for (i = 0; i < arrGiaTri.length; i++) 
    {
        for (j = 0; j < arrGiaTri[i].length; j++) 
        {
            if (isFinite(arrGiaTri[i][j]) && arrGiaTri[i][j] != "") 
            {
                arrGiaTri[i][j] = parseFloat(arrGiaTri[i][j]);
            }
        }
    }
}

function Bang_Ready() {
    var keys = new KeyTable({
        "table": document.getElementById('BangDuLieu')
    });

    Bang_KhoiTao();

    Bang_HienThiDuLieu();

    $('.edit').each(function () {
        keys.event.action(this, function (nCell) {
            /* Block KeyTable from performing any events while jEditable is in edit mode */
            keys.block = true;

            var h0 = nCell.parentNode.rowIndex - BangDuLieu_h0;
            var c0 = nCell.cellIndex - BangDuLieu_c0;
            var strType = "text";
            if (arrKieuDuLieu[h0][c0] == '4') {
                strType = "datetime";
            }
            else if (arrKieuDuLieu[h0][c0] == '5') {
                strType = "date";
            }

            /* Initialise the Editable instance for this table */
            $(nCell).editable(function (sVal) {
                var h = nCell.parentNode.rowIndex - BangDuLieu_h0;
                var c = nCell.cellIndex - BangDuLieu_c0;
                var MaHang = arrMaHang[h];
                var MaCot = arrMaCot[c];
                var TenHam = 'GH' + MaHang + '_' + MaCot;
                if (GanGiaTriO(MaHang, MaCot, sVal)) {
                    var fn = window[TenHam];
                    if (typeof fn == 'function') {
                        fn();
                    }
                }
                sVal = Bang_DinhDangHienThiO(h, c);
                keys.block = false;
                keys.fnSetFocusNextCell();
                return sVal;
            }, {
                "onblur": 'submit',
                "type": strType,
                "onreset": function () {
                    /* Unblock KeyTable, but only after this 'esc' key event has finished. Otherwise
                    * it will 'esc' KeyTable as well
                    */
                    setTimeout(function () { keys.block = false; }, 0);
                }
            });

            /* Dispatch click event to go into edit mode - Saf 4 needs a timeout... */
            setTimeout(function () { $(nCell).click(); }, 0);
        });
    });
}

function Bang_HienThiDuLieuO(h, c) {
    var x = BangDuLieu_c0 + c;
    var y = BangDuLieu_h0 + h;
    objBangDuLieu.rows[y].cells[x].innerHTML = Bang_DinhDangHienThiO(h,c);
}

function Bang_HienThiDuLieu()
{
    var h0 = BangDuLieu_h0;
    var c0 = BangDuLieu_c0;
    for (var h = 0; h < arrGiaTri.length; h++) {
        for (var c = 0; c < BangDuLieu_nC; c++) {
            Bang_HienThiDuLieuO(h, c);
        }
    }
}

function Bang_HamTruocKhiKetThuc() {
    Bang_GanMangGiaTri_arrGiaTri();
    if (document.getElementById("sGhiChuTongHop")) {
        document.getElementById("idGhiChuTongHop").value = document.getElementById("sGhiChuTongHop").value;
    }
    if (document.getElementById("idGhiChu")) {
        document.getElementById("idGhiChu").value = document.getElementById("sGhiChu").value;
    }
    document.getElementById("btnXacNhanGhi").click();
    return true;
}

function Bang_GanMangGiaTri_arrGiaTri() {
    var strTG = "";
    for (var i = 0; i < arrGiaTri.length; i++) {
        if (i>0) strTG += Bang_DauCachHang;
        for (var j = 0; j < arrGiaTri[i].length; j++) {
            if (j > 0) strTG += Bang_DauCachO;
            strTG += arrGiaTri[i][j];
        }
    }
    document.getElementById("idXauGiaTriChiTiet").value = strTG;
}

function Bang_GTBangKhac(TenTruong) {
    return 0;
}

function Bang_LayMangGiaTri(id)
{
    var arr = new Array();
    var strTG = document.getElementById(id).value;
    var arrTG = strTG.split(Bang_DauCachHang);
    for (var i = 0; i < arrTG.length; i++) {
        arr[i] = arrTG[i].split(Bang_DauCachO);
    }
    return arr;
}

function LayGiaTriO(MaHang, MaCot) {
    var vR = 0;
    var h = arrCSMaHang[MaHang];
    var c = arrCSMaCot[MaCot];
    if (h > -1 && c > -1) {
        if (arrKieuDuLieu[h][c] == "4" || arrKieuDuLieu[h][c] == "5") {
            vR = arrGiaTri[h][c];
        }
        else if (isFinite(arrGiaTri[h][c]) && arrGiaTri[h][c] != "") {
            vR = parseFloat(arrGiaTri[h][c]);
        }
        else {
            vR = 0;
        }
    }
    return vR;
}

function Bang_DinhDangHienThiO(h, c) {
    var vR = arrGiaTri[h][c];
    var gt = arrGiaTri[h][c];

    //0:Kieu xau; 1: Kieu so; 2: ;3: Textarea; 4:datetime; 5:date; 6:month;
    if(arrKieuDuLieu[h][c]=="4")
    {
        vR = FormatDatetime(gt,"HH:mm dd/MM/yyyy");
    }
    else if(arrKieuDuLieu[h][c]=="5")
    {
        vR = FormatDatetime(gt,"dd/MM/yyyy");
    }
    else if (gt != "" && isFinite(gt)) {
        //Kieu xau
        arrGiaTri[h][c] = parseFloat(gt);
        vR = FormatNumber(gt);
    }
    return vR;
}

function GanGiaTriO(MaHang, MaCot, GiaTri) {
    var h = arrCSMaHang[MaHang];
    var c = arrCSMaCot[MaCot];
    var ok = true, value;
    if (h > -1 && c > -1) {
        value = "";
        if (arrKieuDuLieu[h][c] == "4" || arrKieuDuLieu[h][c] == "5") {
            value = ParseDatetime(GiaTri);
        }
        else if (IsNumber(GiaTri)) {
            value = ParseNumber(GiaTri);
        }
        else if(arrKieuDuLieu[h][c]!="1"){
            value = GiaTri;
        }
        if (arrGiaTri[h][c] == "" || value == "") {
            arrGiaTri[h][c] = value;
        }
        else if (arrGiaTri[h][c] == value) {
            ok = false;
        }
        else {
            arrGiaTri[h][c] = value;
        }
    }
    return ok;
}

function GanGiaTriThatChoO(MaHang, MaCot, GiaTri) {
    var h = arrCSMaHang[MaHang];
    var c = arrCSMaCot[MaCot];
    if (h > -1 && c > -1) {
        arrGiaTri[h][c] = GiaTri;
        Bang_HienThiDuLieuO(h, c);
        return true;
    }
    return false;
}

function LayChiSo(arr, id) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == id) {
            return i;
        }
    }
    return -1;
}