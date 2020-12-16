using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public abstract class RouteDecorator: Route
    {
        private Route route;

        public RouteDecorator(Route route)
        {
            this.route = route;
            if (this != route) Rebuild(route);
        }

    }
}
