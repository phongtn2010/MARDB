﻿var ServerUrl = "";
var jsMyip_Client = "";

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

function jsBooks_SessionLanguage(iMaNgonNgu) {
    $.ajaxSetup({ cache: false });
    var url = ServerUrl + "/Language/Check_Language?iID_MaNgonNgu=" + iMaNgonNgu;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        var _sCode = item._sCode;
        var imgLanguage = document.getElementById("imgLanguage");
        imgLanguage.src = ServerUrl + "/XML/" + _sCode + ".gif";

        //var div_result = document.getElementById("spanLanguage");
        //div_result.innerHTML = item._sLanguage;


        setTimeout("window.location.reload();", 1000);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsBVND_SessionLanguage_Home(iMaNgonNgu) {
    $.ajaxSetup({ cache: false });
    var url = ServerUrl + "/Language/Check_Language?iID_MaNgonNgu=" + iMaNgonNgu;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        var _sCode = item._sCode;

        setTimeout("window.location.reload();", 1000);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsLoadCaptcha() {
    var sURL = ServerUrl + "/Public/generateCaptcha";
    $.ajax({
        type: 'GET', url: sURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            $("#m_imgCaptcha").attr('src', ServerUrl + "/" + data);
        },
        error: function (data) { alert("Error while loading captcha image") }
    });
}

function jsSetPageSize(iGiaTri) {
    var url = ServerUrl + "/Public/SetPageSize?sGiaTri=" + iGiaTri;
    $.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        var _sCode = item._sCode;

        setTimeout("window.location.reload();", 500);
        //setInterval(function () { window.location.reload(); }, 1000);
    });

    return false;
}

function jsEbank_AddToCart(sMaSanPhan, sCode, rSoLuong, rDonGia) {
    var url = ServerUrl + "/Orders/Insert_User_Cart?iID_MaSanPham=" + sMaSanPhan + "&sCodeSanPham=" + sCode + "&rSoLuong=" + rSoLuong + "&rDonGia=" + rDonGia;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
        //HT lại luot đat
        jsEbank_Count_Cart();
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_UpdateToCart(sMaGioHang_Temp, iSoLuong) {
    var url = ServerUrl + "/Orders/Update_User_Cart?iID_GioHang_Temp=" + sMaGioHang_Temp + "&iSoLuong=" + iSoLuong;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
    });
}

function jsEbank_DeleteToCart(sMaGioHang_Temp) {
    var url = ServerUrl + "/Orders/Delete_User_Cart?iID_GioHang_Temp=" + sMaGioHang_Temp;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        if (item.sCode == 10) {
            window.location = ServerUrl + "/Account/Logon";
        }
        //HT lại luot đat
        jsEbank_Count_Cart();
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_Count_Cart() {
    var url = ServerUrl + "/Orders/Get_Count_User_Cart";
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        if (item.sCode == 1) {
            document.getElementById("spanCount").innerHTML = item.sMess;
            var spanSumMoney = document.getElementById("spanSumMoney");
            spanSumMoney.innerHTML = item.sSum;

            document.getElementById("spanCount1").innerHTML = item.sMess;
            var spanSumMoney1 = document.getElementById("spanSumMoney1");
            spanSumMoney1.innerHTML = item.sSum;

            var div_result = document.getElementById("listProductOrder");
            div_result.innerHTML = item.sHTML;

        }

    });
}

function jsEbank_Cart_VanDon(iGioHang, divID) {
    var url = ServerUrl + "/Orders/Get_User_Cart_VanDon?iID_MaGioHang=" + iGioHang;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }

        if (item.sCode == 1) {
            var div_result = document.getElementById(divID);
            div_result.innerHTML = item.sHTML;
        }
    });
}


function jsEbank_AddToFevorites(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Insert_User_Like?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_DeleteToFevorites(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Delete_User_Like?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
        window.location = ServerUrl + "/Members/Favorites";
    });
}

function jsEbank_AddToCompare(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Insert_User_Compare?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}

function jsEbank_DeleteToCompare(sMaSanPhan) {
    var url = ServerUrl + "/Orders/Delete_User_Compare?iID_MaSanPham=" + sMaSanPhan;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
        window.location = ServerUrl + "/Members/Compare";
    });
}

function jsEbank_AddToDanhGia(sMaSanPhan, iSao, sHoTen, sEmail, sNoiDung) {
    var url = ServerUrl + "/Orders/Insert_User_DanhGia?iID_MaSanPham=" + sMaSanPhan + "&iSao=" + iSao + "&sHoTen=" + sHoTen + "&sEmail=" + sEmail + "&sNoiDung=" + sNoiDung;
    jQuery.getJSON(url, function (item) {
        if (item == null) {
            return null;
        }
        //if (item.sCode == 10) {
        //    window.location = ServerUrl + "/Account/Logon";
        //}
        //Thong bao
        alert(item.sMess);
    });
}

function clearFromSearch() {
    document.getElementById("formSearch").reset();
    //ClearTextbox("#formSearch");
    clearForm(document.getElementById("formSearch"));
}

function ClearTextbox(ParentControl) {
    var control = $(ParentControl).get(0);
    var list = control.getElementsByTagName("input");
    var textAreaList = control.getElementsByTagName("textarea");
    var selectList = control.getElementsByTagName("select");
    for (var i = 0; i < list.length; i++) {
        var attr = list[i].getAttribute('type');
        var control = document.getElementById(list[i].id);
        switch (attr) {
            case "text":
                $(control).val('');
                break;

            case "hidden":
                $(control).val('');
                break;

            case "checkbox":
                control.checked = false;
                break;

            case "radio":
                control.checked = false;
                break;

            case "range":
                control.setAttribute('value', '0');
                break;

            case "file":
                control.setAttribute('value', '');
                break;
        }
    }

    for (var i = 0; i < textAreaList.length; i++) {
        var control = document.getElementById(textAreaList[i].id);
        $(control).val('');
    }
    for (var i = 0; i < selectList.length; i++) {
        $(selectList[i]).val('');
    }
}

function clearForm(myFormElement) {
    var elements = myFormElement.elements;
    myFormElement.reset();
    for (i = 0; i < elements.length; i++) {
        field_type = elements[i].type.toLowerCase();
        switch (field_type) {
            case "text":
            case "password":
            case "textarea":
            case "hidden":
                elements[i].value = "";
                break;
            case "radio":
            case "checkbox":
                if (elements[i].checked) {
                    elements[i].checked = false;
                }
                break;

            case "select-one":
            case "select-multi":
                elements[i].selectedIndex = -1;
                break;
            default:
                break;
        }
    }
}

function showToast_Success() {
    $.toast({
        heading: 'Thông báo!',
        icon: 'success',
        text: 'Bạn đã thao tác thành công!',
        position: 'bottom-right',
        stack: false
    })
}

function showToast_Warning() {
    $.toast({
        heading: 'Cảnh báo!',
        icon: 'warning',
        text: 'Cảnh báo thao tác của bạn không đúng!',
        position: 'bottom-right',
        stack: false
    })
}

function showToast_Error() {
    $.toast({
        heading: 'Thông báo lỗi!',
        icon: 'error',
        text: 'Bạn đã thao tác không thành công!',
        position: 'bottom-right',
        stack: false
    })
}

function showToast_Info() {
    $.toast({
        heading: 'Thông tin chi tiết!',
        icon: 'info',
        text: 'Bạn cần đọc kỹ thông tin trước khi thêm!',
        position: 'bottom-right',
        stack: false
    })
}


function loadPDF_Old(sFile) {
    var pdfScale = 1; // make pdfScale a global variable
    var url = sFile;
    pdfjsLib.GlobalWorkerOptions.workerSrc = ServerUrl + '/Content/plugins/pdf/pdf.worker.js';

    var pdfDoc = null,
        pageNum = 1,
        pageRendering = false,
        pageNumPending = null,
        scale = pdfScale,
        canvas = document.getElementById('the-canvas'),
        ctx = canvas.getContext('2d');

    /**
    * Get page info from document, resize canvas accordingly, and render page.
    * param num Page number.
    */
    function renderPage(num) {
        pageRendering = true;
        // Using promise to fetch the page
        pdfDoc.getPage(num).then(function (page) {
            var viewport = page.getViewport({ scale: scale, });
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            // Render PDF page into canvas context
            var renderContext = {
                canvasContext: ctx,
                viewport: viewport,
            };
            var renderTask = page.render(renderContext);

            // Wait for rendering to finish
            renderTask.promise.then(function () {
                pageRendering = false;
                if (pageNumPending !== null) {
                    // New page rendering is pending
                    renderPage(pageNumPending);
                    pageNumPending = null;
                }
            });
        });

        // Update page counters
        document.getElementById('page_num').textContent = num;
    }

    /**
    * If another page rendering in progress, waits until the rendering is
    * finised. Otherwise, executes rendering immediately.
    */
    function queueRenderPage(num) {
        if (pageRendering) {
            pageNumPending = num;
        } else {
            renderPage(num);
        }
    }

    /**
    * Displays previous page.
    */
    function onPrevPage() {
        if (pageNum <= 1) {
            return;
        }
        pageNum--;
        queueRenderPage(pageNum);
    }
    document.getElementById('prev').addEventListener('click', onPrevPage);

    /**
    * Displays next page.
    */
    function onNextPage() {
        if (pageNum >= pdfDoc.numPages) {
            return;
        }
        pageNum++;
        queueRenderPage(pageNum);
    }
    document.getElementById('next').addEventListener('click', onNextPage);

    /**
    * Displays zoomin.
    */
    function onZoomin() {
        pdfScale = pdfScale + 0.25;
        queueRenderPage(pageNum);
    }
    document.getElementById('zoominbutton').addEventListener('click', onZoomin);

    /**
    * Displays zoomin.
    */
    function onZoomout() {
        if (pdfScale <= 0.25) {
            return;
        }
        pdfScale = pdfScale - 0.25;
        queueRenderPage(pageNum);
    }
    document.getElementById('zoomoutbutton').addEventListener('click', onZoomout);

    /**
    * Asynchronously downloads PDF.
    */
    var loadingTask = pdfjsLib.getDocument(url);
    loadingTask.promise.then(function (pdfDoc_) {
        pdfDoc = pdfDoc_;
        document.getElementById('page_count').textContent = pdfDoc.numPages;

        // Initial/first page rendering
        renderPage(pageNum);
    });
}

function loadPDF(sFile) {
    var myState = {
        pdf: null,
        currentPage: 1,
        zoom: 1
    }

    pdfjsLib.getDocument(sFile).then((pdf) => {

        myState.pdf = pdf;
        render();

    });

    function render() {
        myState.pdf.getPage(myState.currentPage).then((page) => {

            var canvas = document.getElementById("pdf_renderer");
            var ctx = canvas.getContext('2d');

            var viewport = page.getViewport(myState.zoom);

            canvas.width = viewport.width;
            canvas.height = viewport.height;

            page.render({
                canvasContext: ctx,
                viewport: viewport
            });
        });
    }

    function rotateSinglePage(pageNum, degree) {
        var viewerMain = document.getElementById("pdf_renderer");
        var pageContainer = viewerMain.querySelector("div[data-page-number='" + pageNum + "']");
        pageContainer.style.transform = "rotate(" + degree + "deg)";
    }

    document.getElementById('go_previous').addEventListener('click', (e) => {
        if (myState.pdf == null || myState.currentPage == 1)
            return;
        myState.currentPage -= 1;
        document.getElementById("current_page").value = myState.currentPage;
        render();
    });

    document.getElementById('go_next').addEventListener('click', (e) => {
        if (myState.pdf == null || myState.currentPage > myState.pdf._pdfInfo.numPages)
            return;
        myState.currentPage += 1;
        document.getElementById("current_page").value = myState.currentPage;
        render();
    });

    document.getElementById('current_page').addEventListener('keypress', (e) => {
        if (myState.pdf == null) return;

        // Get key code
        var code = (e.keyCode ? e.keyCode : e.which);

        // If key code matches that of the Enter key
        if (code == 13) {
            var desiredPage =
                document.getElementById('current_page').valueAsNumber;

            if (desiredPage >= 1 && desiredPage <= myState.pdf._pdfInfo.numPages) {
                myState.currentPage = desiredPage;
                document.getElementById("current_page").value = desiredPage;
                render();
            }
        }
    });

    document.getElementById('zoom_in').addEventListener('click', (e) => {
        if (myState.pdf == null) return;
        myState.zoom += 0.5;
        render();
    });

    document.getElementById('zoom_out').addEventListener('click', (e) => {
        if (myState.pdf == null) return;
        myState.zoom -= 0.5;
        render();
    });
}

function loadPDFXNCL(sFile) {
    var myState = {
        pdf: null,
        currentPage: 1,
        zoom: 1
    }

    pdfjsLib.getDocument(sFile).then((pdf) => {

        myState.pdf = pdf;
        render();

    });

    function render() {
        myState.pdf.getPage(myState.currentPage).then((page) => {

            var canvas = document.getElementById("pdf_renderer_xncl");
            var ctx = canvas.getContext('2d');

            var viewport = page.getViewport(myState.zoom);

            canvas.width = viewport.width;
            canvas.height = viewport.height;

            page.render({
                canvasContext: ctx,
                viewport: viewport
            });
        });
    }

    document.getElementById('go_previous_xncl').addEventListener('click', (e) => {
        if (myState.pdf == null || myState.currentPage == 1)
            return;
        myState.currentPage -= 1;
        document.getElementById("current_page_xncl").value = myState.currentPage;
        render();
    });

    document.getElementById('go_next_xncl').addEventListener('click', (e) => {
        if (myState.pdf == null || myState.currentPage > myState.pdf._pdfInfo.numPages)
            return;
        myState.currentPage += 1;
        document.getElementById("current_page_xncl").value = myState.currentPage;
        render();
    });

    document.getElementById('current_page_xncl').addEventListener('keypress', (e) => {
        if (myState.pdf == null) return;

        // Get key code
        var code = (e.keyCode ? e.keyCode : e.which);

        // If key code matches that of the Enter key
        if (code == 13) {
            var desiredPage =
                document.getElementById('current_page_xncl').valueAsNumber;

            if (desiredPage >= 1 && desiredPage <= myState.pdf._pdfInfo.numPages) {
                myState.currentPage = desiredPage;
                document.getElementById("current_page_xncl").value = desiredPage;
                render();
            }
        }
    });

    document.getElementById('zoom_in_xncl').addEventListener('click', (e) => {
        if (myState.pdf == null) return;
        myState.zoom += 0.5;
        render();
    });

    document.getElementById('zoom_out_xncl').addEventListener('click', (e) => {
        if (myState.pdf == null) return;
        myState.zoom -= 0.5;
        render();
    });
}


