using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class DirectionFormatter
    {
        protected string GetDirection(string first, string last) => String.Format("{0} - {1}", first, last);
    }
}