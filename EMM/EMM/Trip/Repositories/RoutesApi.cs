using EMM.Helpers;
using EMM.Models;
using EMM.Services.Repositories;
using ImmutableObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Trip.Repositories
{
    public class RoutesApi : IApiRepository<Route>
    {
        private readonly IJsonRepository jsonRepository;
        private Action<HttpResponseMessage> validResponse;

        public RoutesApi(IJsonRepository jsonRepository)
        {
            this.jsonRepository = jsonRepository;
            validResponse = (response) => { if (!response.IsSuccessStatusCode) throw new HttpResponseException(response); };
        }
        public RoutesApi(string accessToken, HttpClient client):this(new JsonStringsApi(accessToken, client, "http://www.emmvi.online/api/Routes/"))
        {
        }
        public async Task Send(Route entity, HttpMethod method)
        {
            var operation = jsonRepository.JsonOperation(JsonConvert.SerializeObject(entity), method);
            operation.Send();
            var response = await operation.Response();
            validResponse(response);
        }

        public async Task<IEnumerable<Route>> Entities()
        {
            string json = await jsonRepository.Entities();
            var routes = JsonConvert.DeserializeObject<IEnumerable<Route>>(json);
            return routes;
        }

    }
}
