using System.Web;
using GoogleMaps.LocationServices;
using SmartASSWeb.Bll;

namespace SmartASSWeb.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfile Profile { get; set; }
        public int TotalContacts { get; set; }
        public int RequestCount { get; set; }
        public HttpPostedFileBase ProfileImageBase { get; set; }
        public string ImagePath { get; set; }

        public string GetMapPath(string address)
        {
            if (string.IsNullOrEmpty(address)) return string.Empty;
            var gls = new GoogleLocationService();
            // var gls = new GoogleLocationService(apikey: "YOUR API KEY");
            var latlong = gls.GetLatLongFromAddress(address);
            var Latitude = latlong.Latitude;
            var Longitude = latlong.Longitude;
            return "";
        }
    }
}