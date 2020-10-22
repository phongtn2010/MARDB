
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DATA0200025.WebServices;

namespace APP0200025.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MardService : WebService
    {
        private readonly WsProcessingService _processingService;

        public MardService()
        {
            //_processingService = WebApiConfig.Container.Resolve<WsProcessingService>();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
