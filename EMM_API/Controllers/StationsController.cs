using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMM_API.Controllers
{
    public class StationsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var context = new EmmDataContext();
            var stations = context.Stations_TS.Select(station => station.name).ToList();
            return stations;
        }

    }

}

