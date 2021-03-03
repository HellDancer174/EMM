using EMM.Helpers;
using EMM.Models;
using EMM.Trip.Routes.MicroClasses;
using ImmutableObject.Requests.JsonRequests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Trip.Routes
{
    public class HttpTransferableRoute : RouteDecorator
    {
        protected JsonRequest jsonRequest;
        public HttpTransferableRoute(Route route, HttpClient client, EmmUrl url, string accessToken) : base(route)
        {
            Action<HttpResponseMessage> validResponse = (response) => { if (!response.IsSuccessStatusCode) throw new HttpResponseException(response); };
            jsonRequest = new AuthorizedJsonRequest(new ValidRequest(new JsonRequest(client, url.ToString()), validResponse), accessToken);
        }
        public override async Task Transfer()
        {
            await jsonRequest.Response();
        }
    }
}
