using EMM.Helpers;
using EMM.Models;
using ImmutableObject;
using ImmutableObject.Requests.JsonRequests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Trip.Repositories
{
    public class RoutesApiRepository: IAsyncRepository<Route>
    {
        private Action<HttpResponseMessage> validResponse;
        private readonly string accessToken;
        private readonly HttpClient client;
        private string url = "http://www.emmvi.online/api/Routes/";

        public RoutesApiRepository(string accessToken, HttpClient client)
        {
            validResponse = (response) => { if (!response.IsSuccessStatusCode) throw new HttpResponseException(response); };
            this.accessToken = accessToken;
            this.client = client;
        }

        public async Task<IEnumerable<Route>> Entities()
        {
            var request = new AuthorizedJsonRequest(new ValidRequest(new JsonRequest(client, url), validResponse), accessToken);
            var response = await request.Response();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Route>>(json);
        }

        //public async Task Add(Route entity)
        //{
        //    var request = new PostJsonRequest(new AuthorizedJsonRequest(new ValidRequest(new JsonRequest(client, url), validResponse), accessToken), JsonConvert.SerializeObject(entity));
        //    request.FormRequest();
        //    await request.Response();
        //}

        //public async Task Update(Route entity)
        //{
        //    var request = new PutJsonRequest(new AuthorizedJsonRequest(new ValidRequest(new JsonRequest(client, url), validResponse), accessToken), JsonConvert.SerializeObject(entity));
        //    request.FormRequest();
        //    await request.Response();
        //}

        //public async Task Remove(Route entity)
        //{
        //    var request = new DeleteJsonRequest(new AuthorizedJsonRequest(new ValidRequest(new JsonRequest(client, url), validResponse), accessToken), JsonConvert.SerializeObject(entity));
        //    request.FormRequest();
        //    await request.Response();
        //}
    }
}
