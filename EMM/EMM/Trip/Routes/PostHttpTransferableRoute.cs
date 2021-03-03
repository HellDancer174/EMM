using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EMM.Models;
using EMM.Trip.Routes.MicroClasses;
using Newtonsoft.Json;
using ImmutableObject.Requests.JsonRequests;

namespace EMM.Trip.Routes
{
    public class PostHttpTransferableRoute : HttpTransferableRoute
    {
        public PostHttpTransferableRoute(Route route, HttpClient client, EmmUrl url, string accessToken) : base(route, client, url, accessToken)
        {
            jsonRequest = new PostJsonRequest(jsonRequest, JsonConvert.SerializeObject(route));
        }

    }
}
