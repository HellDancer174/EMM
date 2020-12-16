using EMM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMM_API.Controllers
{
    public class DirectionsController : ApiController
    {
        public IEnumerable<Direction> Get()
        {
            var dbDirection = new DBDirections();
            var list = new List<Direction>() { dbDirection.CreateDirection("Челябинск", "Кропачево"),
                dbDirection.CreateDirection("Челябинск", "Петропавловск"), dbDirection.CreateDirection("Челябинск", "Екатеринбург"), dbDirection.CreateDirection("Челябинск", "Карталы") };
            return list;
        }

    }
}
