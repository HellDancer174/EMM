using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMM_API.Controllers
{
    public class LocomotivesController : ApiController
    {
        public IDictionary<string, int> Get()
        {
            var context = new EmmDataContext();
            var locType = context.Locomitive_TS.ToDictionary(loc => loc.mk, loc => loc.sectionCount);
            return locType;
        }

    }
}
