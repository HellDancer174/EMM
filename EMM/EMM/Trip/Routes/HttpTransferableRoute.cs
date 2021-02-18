using EMM.Models;
using ImmutableObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Trip.Routes
{
    public class HttpTransferableRoute : RouteDecorator
    {
        private readonly IHttpRequest request;

        public HttpTransferableRoute(Route route, IHttpRequest request) : base(route)
        {
            this.request = request;
        }

        public override async Task Transfer()
        {
            request.Send();
            await request.Response();
        }
    }
}
