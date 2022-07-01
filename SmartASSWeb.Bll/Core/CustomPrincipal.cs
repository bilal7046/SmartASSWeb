using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SmartASSWeb.Bll.Core
{
    public interface ICustomPrincipal
        : IPrincipal
    {
        string UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PhotoUrl { get; set; }
    }

    public class CustomPrincipal
        : ICustomPrincipal
    {
        private string _photoUrl;
        public IIdentity Identity { get; private set; }
      

        public bool IsInRole(string role)
        {
            //string[] packages = {
            //    "Individual",
            //    "SmallBusiness",
            //    "Business",
            //    "Enterprise",
            //    "AllAccess"
            //};
            return role == this.Package;
        }
        public   int GetAccesLevel()
        {
            if (this.Package==Common.INDIVIDUAL)
            {
                return 1;
            }
            else if(this.Package == Common.SMALLBUSINESS)
            {
                return 2;
            }
            else if (this.Package == Common.BUSINESS)
            {
                return 3;
            }
            else
            {
                return 3;
            }
        }
        public string GetPath()
        {
            var a = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(a, "Menu/Menu.json");
            return path;
        }
        public string GetSymbol()
        {
            
            return "&#36";
        }
        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Package { get; set; }

        public string PhotoUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_photoUrl))
                    return Defaults.DefaultProfileImage;
                else
                    return _photoUrl;
            }
            set => _photoUrl = value;
        }
    }

    public class CustomPrincipalSerializeModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Package { get; set; }
    }
}