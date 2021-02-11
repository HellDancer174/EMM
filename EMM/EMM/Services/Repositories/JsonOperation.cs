using ImmutableObject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services.Repositories
{
    public class JsonOperation : IJsonOperation
    {
        private readonly string jsonEntity;
        private readonly HttpMethod method;
        private readonly string accessToken;
        private readonly HttpClient client;
        private readonly string apiUrl;
        private HttpRequestMessage request;

        public JsonOperation(string jsonEntity, HttpMethod method, string accessToken, HttpClient client, string apiUrl)
        {
            this.jsonEntity = jsonEntity;
            this.method = method;
            this.accessToken = accessToken;
            this.client = client;
            this.apiUrl = apiUrl;
        }

        public async Task<HttpResponseMessage> Response()
        {
            return await client.SendAsync(request);
        }

        public void Send()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(jsonEntity);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request = new HttpRequestMessage(method, apiUrl);
            request.Content = content;
        }
    }
}
