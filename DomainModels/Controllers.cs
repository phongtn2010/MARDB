using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DomainModel
{
    public class Controllers
    {
        public abstract class BaseController : Controller
        {
            public virtual string RenderPartialViewToString()
            {
                return RenderPartialViewToString(null, null);
            }
            public virtual string RenderPartialViewToString(string viewName)
            {
                return RenderPartialViewToString(viewName, null);
            }
            public virtual string RenderPartialViewToString(object model)
            {
                return RenderPartialViewToString(null, model);
            }
            public virtual string RenderPartialViewToString(string viewName, object model)
            {
                //Original source code: http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
                if (string.IsNullOrEmpty(viewName))
                    viewName = this.ControllerContext.RouteData.GetRequiredString("action");

                this.ViewData.Model = model;

                using (var sw = new StringWriter())
                {
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                    var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            private string RenderPartialView(string partialViewName, object model)
            {
                if (ControllerContext == null)
                    return string.Empty;

                if (model == null)
                    throw new ArgumentNullException("model");

                if (string.IsNullOrEmpty(partialViewName))
                    throw new ArgumentNullException("partialViewName");

                ViewData.Model = model;

                using (var sw = new StringWriter())
                {
                    var viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            public virtual JsonResult JsonView(bool success, string viewName, object model)
            {
                return Json(new { Success = success, View = RenderPartialView(viewName, model) });
            }
            protected void SaveSelectedTabName(string tabName = "", bool persistForTheNextRequest = true)
            {
                //keep this method synchronized with
                //"GetSelectedTabName" method of \Big.Web.Framework\HtmlExtensions.cs
                if (string.IsNullOrEmpty(tabName))
                {
                    tabName = this.Request.Form["selected-tab-name"];
                }

                if (!string.IsNullOrEmpty(tabName))
                {
                    const string dataKey = "hnd.selected-tab-name";
                    if (persistForTheNextRequest)
                    {
                        TempData[dataKey] = tabName;
                    }
                    else
                    {
                        ViewData[dataKey] = tabName;
                    }
                }
            }
            protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
            {
                AddNotification(NotifyType.Success, message, persistForTheNextRequest);
            }
            protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
            {
                AddNotification(NotifyType.Error, message, persistForTheNextRequest);
            }
            protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
            {
                string dataKey = string.Format("hnd.notifications.{0}", type);
                if (persistForTheNextRequest)
                {
                    if (TempData[dataKey] == null)
                        TempData[dataKey] = new List<string>();
                    ((List<string>)TempData[dataKey]).Add(message);
                }
                else
                {
                    if (ViewData[dataKey] == null)
                        ViewData[dataKey] = new List<string>();
                    ((List<string>)ViewData[dataKey]).Add(message);
                }
            }
        }
    }
}
