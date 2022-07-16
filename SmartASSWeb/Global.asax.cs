using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
   
        
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = AuthTicket(authCookie);

                if (authTicket == null) return;
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

                var newUser = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    FirstName = serializeModel.FirstName,
                    LastName = serializeModel.LastName,
                    Email = serializeModel.Email,
                    Package = serializeModel.Package,
                    PhotoUrl = serializeModel.PhotoUrl
                };

                HttpContext.Current.User = newUser;
            }
        }

        private static FormsAuthenticationTicket AuthTicket(HttpCookie authCookie)
        {
            try
            {
                return FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                return null;
                //throw new Exception("Crytography Error: " + authCookie.Value, ex);
            }
        }

        protected void Application_Error(object sender, CommandEventArgs ex)
        {
            Exception exception = Server.GetLastError();
            if (exception is CryptographicException)
            {
                FormsAuthentication.SignOut();
                Server.ClearError();
            }
        }
    }
}
