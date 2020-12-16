using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services
{
    public class BooleanTryCatcher : GenericTryCatcher<bool>
    {
        protected override bool ReFunction()
        {
            return false;
        }
    }
}
