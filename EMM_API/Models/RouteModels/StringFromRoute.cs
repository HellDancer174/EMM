using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EMM_API.Models.RouteModels
{
    public class StringFromRoute
    {
        public int id;
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public StringFromRoute(DateTime date, string direction, int id)
        {
            this.id = id;
            this.Date = date;
            this.Direction = direction;
        }

    }
}
