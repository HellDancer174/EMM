using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EMM.Services.Requests.JsonRequests
{
    public class JsonCommandRequest : JsonRequest
    {
        protected readonly JsonRequest primary;
        private readonly string jsonEntity;

        public JsonCommandRequest(JsonRequest other, string jsonEntity) : base(other)
        {
            primary = other;
            this.jsonEntity = jsonEntity;
        }

        public override void Send()
        {
            primary.Send();
            var content = new StringContent(jsonEntity);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;
        }
    }
}
