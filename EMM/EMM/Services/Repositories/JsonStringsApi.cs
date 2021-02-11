using EMM.Helpers;
using ImmutableObject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services.Repositories
{
    public class JsonStringsApi : IJsonRepository
    {
        private readonly string accessToken;
        private readonly HttpClient client;
        private readonly string apiUrl;

        public JsonStringsApi(string accessToken, HttpClient client, string apiUrl)
        {
            this.accessToken = accessToken;
            this.client = client;
            this.apiUrl = apiUrl;
        }

        public async Task<string> Entities()
        {
            if (!String.IsNullOrEmpty(accessToken)) client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode) throw new HttpResponseException(response);
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        public IJsonOperation JsonOperation(string jsonEntity, HttpMethod method)
        {
            return new JsonOperation(jsonEntity, method, accessToken, client, apiUrl);
        }
    }
}
