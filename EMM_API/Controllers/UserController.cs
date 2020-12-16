using EMM_API.Models;
using EMM_API.Services;
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
    public class UserController : ApiController
    {
        // GET: api/User
        public User Get()
        {
            var context = ApplicationDbContext.Create();
            string id = User.Identity.GetUserId();
            var catcher = new UserTryCatcher();
            return catcher.Execute(()=>
            {
                var appUser = context.Users.Where(user => user.Id == id).Single();
                return new User(appUser.Position, appUser.Rate, appUser.QualificationClass);
            });

        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public HttpResponseMessage Post([FromBody]User value)
        {
            var context = ApplicationDbContext.Create();
            string id = User.Identity.GetUserId();
            var catcher = new HttpResponseCatcher();
            return catcher.Execute(() =>
            {
                var appUser = context.Users.Where(user => user.Id == id).Single();
                value.RebuildAppUser(appUser);
                context.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            });

        }

        // PUT: api/User/5
        public void Put([FromBody]User value)
        {

        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
