using DATA0200025.WebServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace APP0200025.Controllers
{
    [RoutePrefix("ws")]
    public class GatewayRelayController : ApiController
    {
        [Route("GatewayProxy")]
        [HttpPost]
        public HttpResponseMessage ForwardWsMessage(HttpRequestMessage request)
        {
            var externalRequest = (HttpWebRequest)WebRequest.Create(WsConfig.GatewayUrl);
            CopyTo(HttpContext.Current.Request, externalRequest);
            var externalResponse = (HttpWebResponse)externalRequest.GetResponse();
            var ms = new MemoryStream();
            externalResponse.GetResponseStream().CopyTo(ms);
            ms.Position = 0;
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            using (var reader = new StreamReader(ms))
            {
                var objText = reader.ReadToEnd();
                response.Content = new StringContent(objText, Encoding.UTF8, "text/xml");
                response.Content.Headers.ContentLength = ms.Length;
            }

            return response;
        }

        private static void CopyTo(HttpRequest source, HttpWebRequest destination)
        {
            destination.Method = source.HttpMethod;

            foreach (var headerKey in source.Headers.AllKeys)
                if (!WebHeaderCollection.IsRestricted(headerKey))
                    destination.Headers[headerKey] = source.Headers[headerKey];

            if ((source.AcceptTypes ?? new string[] { }).Any())
                destination.Accept = string.Join(",", source.AcceptTypes);

            destination.ContentType = source.ContentType;
            destination.Referer = source.UrlReferrer?.AbsoluteUri;
            destination.UserAgent = source.UserAgent;

            if (source.HttpMethod == "GET" || source.HttpMethod == "HEAD" || source.ContentLength <= 0) return;
            var destinationStream = destination.GetRequestStream();
            source.InputStream.CopyTo(destinationStream);
            destinationStream.Close();
        }
    }
}
