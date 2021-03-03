using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Trip.Routes.MicroClasses
{
    public class EmmUrl
    {
        private readonly string url;

        public EmmUrl(string url)
        {
            this.url = url;
        }
        public EmmUrl(): this("http://www.emmvi.online/api/Routes/")
        {
        }

        public override string ToString()
        {
            return url;
        }
    }
}
