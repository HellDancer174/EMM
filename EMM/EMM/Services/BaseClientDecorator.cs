using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services
{
    public abstract class BaseClientDecorator: HttpClient
    {
        public abstract new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        public abstract new Task<HttpResponseMessage> DeleteAsync(string url);
        public abstract new Task<HttpResponseMessage> GetAsync(string url);
        public abstract new Task<HttpResponseMessage> PostAsync(string url, HttpContent content);

    }
}
