using System.Net.Http;
using System.Web;
using Microsoft.Owin;

namespace SmartASSWeb.Core
{
    public static class OwinContextHelper
    {
        private static HttpRequestMessage HttpRequestMessage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                }
                return null;
            }
        }
        private static HttpRequestBase HttpRequestBase
        {
            get
            {
                try
                {
                    if (HttpContext.Current != null)
                    {
                        return new HttpRequestWrapper(HttpContext.Current.Request);
                    }
                }
                catch { }
                return null;
            }
        }
        public static IOwinContext OwinContext
        {
            get
            {
                // un comment this if Web API is supported
                //if (HttpRequestMessage != null)
                //{
                // return OwinHttpRequestMessageExtensions.GetOwinContext(HttpRequestMessage);
                //}
                if (HttpRequestBase != null)
                {
                    return HttpContextBaseExtensions.GetOwinContext(HttpRequestBase);
                }
                //throw new NotSupportedException("Getting an Owin Context from the current context is not supported");
                return null;
            }
        }

        public static string CurrentApplicationUser
        {
            get
            {
                if (OwinContext != null)
                {
                    var currentUsername = OwinContext.Authentication.User.Identity.Name;
                    return currentUsername;
                }
                return "SYS";
            }
        }
        
    }
}
