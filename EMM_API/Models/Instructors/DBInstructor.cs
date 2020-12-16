using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models.Instructors
{
    public class DBInstructor
    {
        protected EmmDataContext context;
        protected int routeID;
        protected Routes_TT route;
        protected int updateCount = 0;
    }
}