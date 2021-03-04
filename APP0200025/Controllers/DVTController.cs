using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using DomainModel.Abstract;
using DATA0200025;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace APP0200025.Controllers
{
    public class DVTController : Controller
    {
        // GET: DVT
        public ActionResult Index()
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_DonViTinh", "List") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["smenu"] = 187;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(String iID_MaDonViTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_DonViTinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            ViewData["DuLieuMoi"] = "0";
            if (string.IsNullOrEmpty(iID_MaDonViTinh))
            {
                ViewData["DuLieuMoi"] = "1";
            }
            ViewData["iID_MaDonViTinh"] = CString.SafeString(iID_MaDonViTinh);
            ViewData["smenu"] = 187;
            return View();
        }

        [Authorize, ValidateInput(false), HttpPost]
        public ActionResult Edit(String ParentID, String iID_MaDonViTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_DonViTinh", "Edit") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            NameValueCollection values = new NameValueCollection();
            string sTen = CString.SafeString(Request.Form[ParentID + "_sTen"]);
            string DuLieuMoi = CString.SafeString(Request.Form[ParentID + "_DuLieuMoi"]);

            if (string.IsNullOrEmpty(sTen))
            {
                values.Add("err_sTen", "Bạn chưa nhập tiêu đề!");
            }
            if (values.Count > 0)
            {
                for (int i = 0; i <= (values.Count - 1); i++)
                {
                    ModelState.AddModelError(ParentID + "_" + values.GetKey(i), values[i]);
                }
                base.ViewData["DuLieuMoi"] = "0";
                if (string.IsNullOrEmpty(iID_MaDonViTinh))
                {
                    ViewData["DuLieuMoi"] = "1";
                }
                ViewData["iID_MaDonViTinh"] = CString.SafeString(iID_MaDonViTinh);
                ViewData["smenu"] = 187;
                return View();
            }

            try
            {
                Bang bang = new Bang("CNN25_DonViTinh");
                bang.MaNguoiDungSua = User.Identity.Name;
                bang.IPSua = Request.UserHostAddress;
                bang.TruyenGiaTri(ParentID, Request.Form);

                if (DuLieuMoi == "0")
                {
                    //Sua
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaDonViTinh;
                }
                else
                {
                    //var person = new Person("John Doe", "gardener");

                    //var json = JsonConvert.SerializeObject(person);
                    //var data = new StringContent(json, Encoding.UTF8, "application/json");

                    //var url = "https://httpbin.org/post";
                    //using var client = new HttpClient();

                    //var response = await client.PostAsync(url, data);

                    //string result = response.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(result);

                    //record Person(string Name, string Occupation);

                    //HttpClient client = new HttpClient();
                    //client.BaseAddress = new Uri("http://103.248.160.33/VROOT/controller/mard/category");
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(
                    //    new MediaTypeWithQualityHeaderValue("application/json"));

                    //// Initialization.  
                    //HttpResponseMessage response = new HttpResponseMessage();

                    //// HTTP POST  
                    //response = client.PostAsJsonAsync("api/WebApi", requestObj);

                    //// Verification  
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    // Reading Response.  
                    //    string result = response.Content.ReadAsStringAsync().Result;
                    //    responseObj = JsonConvert.DeserializeObject<DataTable>(result);
                    //}


                    //var client = new RestClient("http://103.248.160.33/VROOT/controller/mard/category");
                    //client.Timeout = -1;
                    //var request = new RestRequest(Method.POST);
                    //request.AddHeader("Content-Type", "application/json");
                    //request.AddParameter("application/json", "{\r\n  \"action\": 1,\r\n  \"fiCatNo\": 3,\r\n  \"fiCatNote\": \"DV76\",\r\n  \"fiCatType\": 0,\r\n  \"fiCatTypeName\": \"mg/15ml\"\r\n} ", ParameterType.RequestBody);
                    //IRestResponse response = client.Execute(request);
                    //Console.WriteLine(response.Content);

                    //Them moi
                    bang.DuLieuMoi = true;
                }
                bang.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Delete(string iID_MaDonViTinh)
        {
            if (BaoMat.ChoPhepLamViec(User.Identity.Name, "CNN25_DonViTinh", "Delete") == false || !CPQ_MENU.CoQuyenXemTheoMenu(Request.Url.AbsolutePath, User.Identity.Name))
            {
                return RedirectToAction("Index", "PermitionMessage");
            }
            try
            {
                if (string.IsNullOrEmpty(iID_MaDonViTinh) == false)
                {
                    SqlCommand cmd;

                    //Xoa danh muc san pham
                    cmd = new SqlCommand("DELETE FROM CNN25_DonViTinh WHERE iID_MaDonViTinh=@iID_MaDonViTinh");
                    cmd.Parameters.AddWithValue("@iID_MaDonViTinh", CString.SafeString(iID_MaDonViTinh));
                    Connection.UpdateDatabase(cmd);
                    cmd.Dispose();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Errors", new { sMess = ex.ToString() });
            }
        }
    }
}