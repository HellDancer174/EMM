using EMM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Services
{
    public class UserTryCatcher : GenericTryCatcher<User>
    {
        protected override User ReFunction()
        {
            return new User("Помощник машиниста электровоза (маневровое движение)", 115.3, 6);
        }
    }
}