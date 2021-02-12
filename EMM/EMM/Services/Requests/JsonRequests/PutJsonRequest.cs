using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EMM.Services.Requests.JsonRequests
{
    public class PutJsonRequest : JsonCommandRequest
    {
        public PutJsonRequest(JsonRequest other, string jsonEntity) : base(other, jsonEntity)
        {
        }

        public override void Send()
        {
            base.Send();
            request.Method = HttpMethod.Put;
        }
    }
}
