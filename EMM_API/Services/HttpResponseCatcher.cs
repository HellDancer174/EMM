using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace EMM_API.Services
{
    public class HttpResponseCatcher: GenericTryCatcher<HttpResponseMessage>
    {
        protected override HttpResponseMessage ReFunction()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = "Запрос не может быть воспринят сервером" }));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}