using EMM.Helpers;
using EMM.Models;
using EMM.Models.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EMM.Views;

namespace EMM.Services
{
    public class ApiServices
    {
        private static HttpClient client;
        public ApiServices()
        {
            client = new HttpClient();
        }


        public async Task RegisterAsync(string email, string password, string confirmPassword, double rate, string position, string qualification)
        {
            var model = new RegisterBindingModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                Rate = rate,
                Position = position,
                QualificationClass = qualification
            };
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("http://www.emmvi.online/api/Account/Register", content);
            if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);

        }
        public async Task ChangePasswordAsync(string oldPass, string newPass, string confirmPass)
        {
            var model = new ChangePasswordBindingModel()
            {
                OldPassword = oldPass,
                NewPassword = newPass,
                ConfirmPassword = confirmPass
            };
            var json = JsonConvert.SerializeObject(model);
            var response = await SendAsync(Settings.AccessToken, json, HttpMethod.Post, "http://www.emmvi.online/api/Account/ChangePassword");
            if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);
            else Settings.Password = newPass;
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var keyvalue = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://www.emmvi.online/Token");
            request.Content = new FormUrlEncodedContent(keyvalue);
            //var client = new HttpClient();
            var response = await client.SendAsync(request);
            var jwt = await response.Content.ReadAsStringAsync();
            JObject jwtObject = JObject.Parse(jwt);
            var accessToken = jwtObject.Value<string>("access_token");
            var expires = jwtObject.Value<DateTime>(".expires");
            Settings.AccessToken = accessToken;
            Settings.AccessTokenExpires = expires;
            if (String.IsNullOrEmpty(accessToken)) throw new HttpResponseException(response, jwtObject.Value<string>("error_description"));
            Debug.WriteLine(jwt);
            return accessToken;
        }
        public async Task<IEnumerable<Route>> GetRoutesAsync(string accessToken)
        {
            var url = "http://www.emmvi.online/api/Routes/";
            string json = await GetAsync(url, accessToken);
            var routes = JsonConvert.DeserializeObject<IEnumerable<Route>>(json);
            return routes;
        }

        public async Task<IEnumerable<string>> GetStationsAsync()
        {
            string jsonStations = await GetAsync("http://www.emmvi.online/api/Stations/");
            var stations = JsonConvert.DeserializeObject<IEnumerable<string>>(jsonStations);
            return stations;
        }
        public async Task<IDictionary<string, int>> GetLocomotivesAsync()
        {
            string jsonLocomotives = await GetAsync("http://www.emmvi.online/api/Locomotives/");
            var locomotives = JsonConvert.DeserializeObject<IDictionary<string, int>>(jsonLocomotives);
            return locomotives;
        }
        public async Task<IDictionary<string, double>> GetRateAsync()
        {
            var jsonRates = await GetAsync("http://www.emmvi.online/api/Rate/");
            var rates = JsonConvert.DeserializeObject<IDictionary<string, double>>(jsonRates);
            return rates;
        }

        public async Task<bool> CreateRouteAsync(string accessToken, Route route)
        {
            HttpResponseMessage response = await SendAsyncForRoute(accessToken, route, HttpMethod.Post, "http://www.emmvi.online/api/Routes/");
            if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
        public async Task<bool> RefreshRouteAsync(string accessToken, Route route)
        {
            HttpResponseMessage response = await SendAsyncForRoute(accessToken, route, HttpMethod.Put, "http://www.emmvi.online/api/Routes/");
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private async Task<HttpResponseMessage> SendAsyncForRoute(string accessToken, Route route, HttpMethod httpMethod, string url)
        {
            //var client = new HttpClient();

            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonRoute = JsonConvert.SerializeObject(route);
            //var content = new StringContent(jsonRoute);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //var request = new HttpRequestMessage(httpMethod, url);
            //request.Content = content;
            //var response = await client.SendAsync(request);
            return await SendAsync(accessToken, jsonRoute, httpMethod, url);
        }
        private async Task<HttpResponseMessage> SendAsync(string accessToken, string json, HttpMethod httpMethod, string url)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var request = new HttpRequestMessage(httpMethod, url);
            request.Content = content;
            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<bool> DeleteRouteAsync(string accessToken, Route route)
        {
            var response = await SendAsyncForRoute(accessToken, route, HttpMethod.Delete, "http://www.emmvi.online/api/Routes/");
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private async Task<string> GetAsync(string url, string accessToken = null)
        {
            //var client = new HttpClient();
            if(!String.IsNullOrEmpty(accessToken)) client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
        public async Task<User> GetUserAsync(string accessToken)
        {
            var url = "http://www.emmvi.online/api/User/";
            string json = await GetAsync(url, accessToken);
            var user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }
        public async Task UpdateUserAsync(string accessToken, User user)
        {
            var url = "http://www.emmvi.online/api/User/";
            var jsonUser = JsonConvert.SerializeObject(user);
            var response = await SendAsync(accessToken, jsonUser, HttpMethod.Post, url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                JObject parsed = JObject.Parse(content);
                var message = parsed.Value<string>("Message");
                throw new HttpResponseException(response, message);
            }

        }

        public async Task<IEnumerable<Direction>> GetDirectionsAsync()
        {
            var jsonDirections = await GetAsync("http://www.emmvi.online/api/Directions/");
            var directions = JsonConvert.DeserializeObject<IEnumerable<Direction>>(jsonDirections);
            return directions;
        }

    }
}
