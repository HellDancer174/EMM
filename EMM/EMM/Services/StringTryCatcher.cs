using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services
{
    public class StringTryCatcher : GenericTryCatcher<string>
    {
        public StringTryCatcher()
        {
        }

        protected override string ReFunction()
        {
            return String.Empty;
        }
    }
}
