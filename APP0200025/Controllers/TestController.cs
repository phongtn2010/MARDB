using DATA0200025;
using DATA0200025.WebServices;
using DATA0200025.WebServices.XmlType.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP0200025.Controllers
{
    public class TestController : Controller
    {
        private SendService _sendService = new SendService();
        // GET: Test
        public ActionResult Index()
        {
            String sTen = "tfsdfdsfsd";

            //long iID_MaFile = CDinhKem.ThemDinhKem(0, 0, 0, "", "", "", "PHONG", "", null, 1, "http://mard.adp-p.com/Files/16c8858e-0f58-4c2c-939f-f7ac40d8cf2f.png", "doanhnghiep", "");

            KetQuaXuLy resultConfirm = new KetQuaXuLy();
            resultConfirm.NSWFileCode = "BNNPTNT25200010180";
            resultConfirm.Reason = "Đã tiêp nhận hồ sơ";
            resultConfirm.AttachmentId = "01";
            resultConfirm.FileName = "File Test";
            resultConfirm.FileLink = "LinkFile";
            resultConfirm.NameOfStaff = "PHONG PHẠM";
            resultConfirm.ResponseDate = DateTime.Now;

            string error = _sendService.KetQuaXuLy("BNNPTNT25200010180", resultConfirm, "06");

            //string sYeuCBS = _sendService.YeuCauBoSung("BNNPTNT25200010060", "Đã tiêp nhận hồ sơ", "PHONG PHẠM");

            if (string.IsNullOrEmpty(error))
            {
                //_hoSoService.Save();
                //_lichSuHoSoService.Save();
                //return request.CreateResponse(HttpStatusCode.OK, new SimpleResponse()
                //{
                //    message = "Đã gửi yêu cầu bổ sung thành công",
                //    data = null
                //});
            }
            else
            {
                //return request.CreateResponse(HttpStatusCode.InternalServerError, new SimpleResponse()
                //{
                //    message = error,
                //    data = null
                //});
            }

            return View();
        }
    }
}