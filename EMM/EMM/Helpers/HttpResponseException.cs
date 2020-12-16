using EMM.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Helpers
{
    public class HttpResponseException: HttpRequestException
    {
        private readonly HttpResponseMessage response;
        private readonly string eror;

        public HttpResponseException(HttpResponseMessage response)
        {
            this.response = response;
            eror = String.Empty;
        }
        public HttpResponseException(HttpResponseMessage response, string eror)
        {
            this.response = response;
            this.eror = eror;
        }
        public bool StatusCodesEquals(HttpStatusCode other) => other == response.StatusCode;
        public async Task PrintEror(ICommandPage page)
        {
            if (!String.IsNullOrEmpty(eror))
            {
                page.PrintErorAsync(String.Format("{0}: {1}", response.StatusCode, eror));
                return;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                page.PrintErorAsync(string.Format("{0}: {1}", response.StatusCode, json));
                return;
            }
        }
    }
}
