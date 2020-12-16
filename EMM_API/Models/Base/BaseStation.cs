using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseStation
    {
        private int id;
        private string name;
        private DateTime depatureTime;
        private DateTime arravalTime;
        private TimeSpan maneuvers;
        public BaseStation()
        {

        }
        public BaseStation(int id, string name, DateTime depature, DateTime arraval, TimeSpan maneuvers)
        {
            this.id = id;
            this.name = name;
            this.depatureTime = depature;
            this.arravalTime = arraval;
            this.maneuvers = maneuvers;
        }

        public void ToAway(Action<int, string, DateTime, DateTime, TimeSpan> instructionsForMe)
        {
            instructionsForMe(id, name, depatureTime, arravalTime, maneuvers);
        }
        public TResult ToAway<TResult>(Func<int, string, DateTime, DateTime, TimeSpan, TResult> instructions)
        {
            return instructions(id, name, depatureTime, arravalTime, maneuvers);
        }

    }
}