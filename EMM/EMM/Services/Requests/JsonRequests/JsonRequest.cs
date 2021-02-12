using ImmutableObject;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services.Requests.JsonRequests
{
    public class JsonRequest : IHttpRequest
    {
        protected readonly HttpClient client;
        private readonly string url;
        protected HttpRequestMessage request;

        public JsonRequest(HttpClient client, string url)
        {
            this.client = client;
            this.url = url;
        }
        public JsonRequest(JsonRequest other)
        {
            this.client = other.client;
            this.url = other.url;
        }

        public virtual async Task<HttpResponseMessage> Response()
        {
            return await client.SendAsync(request);
        }

        public virtual void Send()
        {
            request = new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}
