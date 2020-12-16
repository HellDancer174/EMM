using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMM_API.Controllers
{
    public class RateController : ApiController
    {
        public IDictionary<string, double> Get()
        {
            var context = new RateDataContextDataContext();
            var dict = context.Positions_TS.ToDictionary(position => position.position, position => position.rate);
            return dict;
            
        }
    }
}
