using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartASSWeb.Bll.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<IEnumerable<State>> GetStates(string country);
        Task<IEnumerable<City>> GetCities(string state);
    }
    public class AddressService
        : IAddressService
    {
        private string APIKey = "uSx6_EHXkUuDqLlBmyFpgdYbbYJqNNuIAzGmeSGgV3VAtps4x1OHfdA139gOBZZVerA";
        private string UniversalTutorialComAPIUrl = "https://www.universal-tutorial.com/api/";
        private string APIEmail = "info@smartass.global";
        private readonly MediaTypeWithQualityHeaderValue ApplicationJsonContentType = new MediaTypeWithQualityHeaderValue("application/json");

        public async Task<IEnumerable<Country>> GetCountries()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(UniversalTutorialComAPIUrl) })
            {
                var token = await GetAccessToken(client);
                return await GetData<Country>(client, token, "countries/");
            }
        }

        public async Task<IEnumerable<State>> GetStates(string country)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(UniversalTutorialComAPIUrl) })
            {
                var token = await GetAccessToken(client);
                return await GetData<State>(client, token, $"states/{country}");
            }
        }

        public async Task<IEnumerable<City>> GetCities(string state)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(UniversalTutorialComAPIUrl) })
            {
                var token = await GetAccessToken(client);
                return await GetData<City>(client, token, $"cities/{state}");
            }
        }
        
        #region Helper Methods
        
        private async Task<Token> GetAccessToken(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(ApplicationJsonContentType);
            client.DefaultRequestHeaders.Add("api-token", APIKey);
            client.DefaultRequestHeaders.Add("user-email", APIEmail);
            var authResponse = await client.GetAsync("getaccesstoken");
            string authToken = await authResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(authToken);
        }

        private async Task<IEnumerable<T>> GetData<T>(HttpClient client, Token token, string endpoint)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.auth_token);
            client.DefaultRequestHeaders.Accept.Add(ApplicationJsonContentType);
            var response = await client.GetAsync(endpoint);
            string stringData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T[]>(stringData);
        }

        public class Token
        {
            public string auth_token { get; set; }
        }
        #endregion
    }
    public class Country
    {
        [JsonProperty("country_name")]
        public string Name { get; set; }
        [JsonProperty("country_short_name")]
        public string Code { get; set; }
        [JsonProperty("country_phone_code")]
        public string PhoneCode { get; set; }
    }
    public class State
    {
        [JsonProperty("state_name")]
        public string Name { get; set; }
    }
    public class City
    {
        [JsonProperty("city_name")]
        public string Name { get; set; }
    }
}
