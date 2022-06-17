using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SmartASSWeb.Bll.Services
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetWeatherDetail(string city);
    }
    public class WeatherService
        : IWeatherService
    {
        public async Task<WeatherResult> GetWeatherDetail(string city)
        {
            try
            {
                if (string.IsNullOrEmpty(city)) return null;
                //Assign API KEY which received from OPENWEATHERMAP.ORG  
                string appId = "c2d5712b0ec68cf7e495c880656a8665";

                //API path with CITY parameter and other parameters.  
                string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&cnt=1&APPID={appId}";

                using (WebClient client = new WebClient())
                {
                    string json = await client.DownloadStringTaskAsync(url);

                    //********************//  
                    //     JSON RECEIVED   
                    //********************//  
                    //{"coord":{ "lon":72.85,"lat":19.01},  
                    //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
                    //"base":"stations",  
                    //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
                    //"visibility":2500,  
                    //"wind":{"speed":4.1,"deg":140},  
                    //"clouds":{"all":0},  
                    //"dt":1578730750,  
                    //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
                    //"timezone":19800,  
                    //"id":1275339,  
                    //"name":"Mumbai",  
                    //"cod":200}  

                    //Converting to OBJECT from JSON string.  
                    var weatherInfo = (new JavaScriptSerializer()).Deserialize<Rootobject>(json);

                    //Special VIEWMODEL design to send only required fields not all fields which received from   
                    //www.openweathermap.org api  
                    var model = new WeatherResult
                    {
                        Country = weatherInfo.sys.country,
                        City = weatherInfo.name,
                        Lat = Convert.ToString(weatherInfo.coord.lat, CultureInfo.InvariantCulture),
                        Lon = Convert.ToString(weatherInfo.coord.lon, CultureInfo.InvariantCulture),
                        Description = weatherInfo.weather[0].description,
                        Humidity = Convert.ToString(weatherInfo.main.humidity),
                        Temp = Convert.ToString(Math.Round(weatherInfo.main.temp), CultureInfo.InvariantCulture),
                        TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like, CultureInfo.InvariantCulture),
                        TempMax = Convert.ToString(weatherInfo.main.temp_max, CultureInfo.InvariantCulture),
                        TempMin = Convert.ToString(Math.Round(weatherInfo.main.temp_min), CultureInfo.InvariantCulture),
                        WeatherIcon = weatherInfo.weather[0].icon
                    };


                    //Converting OBJECT to JSON String   
                    //var jsonstring = new JavaScriptSerializer().Serialize(model);

                    //Return JSON string.  
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public class Rootobject
        {
            public Coord coord { get; set; }
            public Weather[] weather { get; set; }
            public string _base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public class Coord
        {
            public float lon { get; set; }
            public float lat { get; set; }
        }

        public class Main
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
            public float temp_min { get; set; }
            public float temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public int sea_level { get; set; }
            public int grnd_level { get; set; }
        }

        public class Wind
        {
            public float speed { get; set; }
            public int deg { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
    }
    public class WeatherResult
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Description { get; set; }
        public string Humidity { get; set; }
        public string TempFeelsLike { get; set; }
        public string Temp { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string WeatherIcon { get; set; }
    }
}
