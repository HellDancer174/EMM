using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services.Requests.JsonRequests
{
    public class AuthorizedJsonRequest : JsonRequest
    {
        private readonly string accessToken;

        public AuthorizedJsonRequest(JsonRequest other, string accessToken) : base(other)
        {
            this.accessToken = accessToken;
        }

        public override void Send()
        {
            base.Send();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
