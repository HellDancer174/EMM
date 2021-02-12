using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EMM.Services.Requests.JsonRequests
{
    public class DeleteJsonRequest : JsonCommandRequest
    {
        public DeleteJsonRequest(JsonRequest other, string jsonEntity) : base(other, jsonEntity)
        {
        }

        public override void Send()
        {
            base.Send();
            request.Method = HttpMethod.Delete;
        }
    }
}
