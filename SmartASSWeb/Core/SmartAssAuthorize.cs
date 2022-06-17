using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SmartASSWeb.Bll;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SmartAssAuthorizeAttribute 
        : AuthorizeAttribute
    {
        private readonly string[] _allowedPackages;
        public SmartAssAuthorizeAttribute(params string[] allowedPackages)
        {
            this._allowedPackages = allowedPackages;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (SkipAuthorization(filterContext)) return;

            if (HttpContext.Current.User.Identity == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var url = new UrlHelper(filterContext.RequestContext).Action("Login", "Account");
                filterContext.Result = new RedirectResult(url);
            }
        }

        private static bool SkipAuthorization(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                   || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (_allowedPackages.Length == 0) return true;

            var email = HttpContext.Current.User.Identity.Name;
            var userProfilePackage = Task.Run(() => GetUserPackage(email)).Result;
            if (string.IsNullOrEmpty(userProfilePackage)) return false;
            var allowedPackages = _allowedPackages[0].Split(',');
            if (userProfilePackage == EnumPackage.AllAccess.ToString()) return true;
            if (allowedPackages.Any(p => p.Contains(userProfilePackage)))
                return true;
         
            return false;
        }

        private async Task<string> GetUserPackage(string email)
        {
            IFirebaseService service = new FirebaseService(new KeyFileResolver());
            var userProfile = await service.Get<UserProfile>(FirestoreTableStore.UserProfiles, "email", email);
            return userProfile?.Package;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}