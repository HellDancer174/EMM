using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Services.Requests.JsonRequests
{
    public class ValidRequest : JsonRequest
    {
        private readonly JsonRequest other;
        private readonly Action<HttpResponseMessage> validResponse;

        public ValidRequest(JsonRequest other, Action<HttpResponseMessage> validResponse) : base(other)
        {
            this.other = other;
            this.validResponse = validResponse;
        }

        public override async Task<HttpResponseMessage> Response()
        {
            var response = await other.Response();
            validResponse(response);
            return response;
        }
    }
}
