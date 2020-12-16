using EMM_API.Models;
using EMM_API.Models.RouteModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMM_API.Controllers
{
    [Authorize]
    public class RoutesController : ApiController
    {
        // GET api/values
        public IEnumerable<Route> Get()
        {
            var userID = User.Identity.GetUserId();
            var routes = new DBRoutes();
            return routes.GetRoutes(userID);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Route route)
        {
            var userID = User.Identity.GetUserId();
            var routes = new DBRoutes();
            var result = routes.CreateRoute(route, userID);
            if (!result)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            else return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // PUT api/values/5
        public void Put([FromBody]Route route)
        {
            var userID = User.Identity.GetUserId();
            var routes = new DBRoutes();
            routes.UpdateRoute(route, userID);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete([FromBody]Route route)
        {
            var userID = User.Identity.GetUserId();
            var routes = new DBRoutes();
            var result = routes.DeleteRoute(route, userID);
            if (!result)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            else return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}
