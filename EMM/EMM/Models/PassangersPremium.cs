using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class PassangersPremium
    {
        private readonly Passangers passangers;

        public PassangersPremium(Passangers passangers)
        {
            this.passangers = passangers;
        }
        public double CalcPassPremium(double rate)
        {
            return passangers.CalcPassangersTime().TotalHours * rate;
        }
        public double CalcWaitPassPremium(double rate)
        {
            return passangers.CalcWaitPassangersTime().TotalHours * rate;
        }
    }
}
