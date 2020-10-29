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

            KetQuaXuLy resultConfirm = new KetQuaXuLy();
            resultConfirm.NSWFileCode = "BNNPTNT25200010060";
            resultConfirm.Reason = "Đã tiêp nhận hồ sơ";
            resultConfirm.AttachmentId = "01";
            resultConfirm.FileName = "File Test";
            resultConfirm.FileLink = "LinkFile";
            resultConfirm.NameOfStaff = "PHONG PHẠM";
            resultConfirm.ResponseDate = DateTime.Now;

            string error = _sendService.KetQuaXuLy("BNNPTNT25200010060", resultConfirm, "06");

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