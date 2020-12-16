using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseLocomotive
    {
        protected int routeID;
        protected string type;
        protected string number;
        protected BaseBackgroundTime times;
        protected List<BaseMeters> meters;
        protected int sectionCount;

        public BaseLocomotive()
        {
            this.times = new BaseBackgroundTime();

        }
        public BaseLocomotive(int routeID, string type, string number, BaseBackgroundTime times, IEnumerable<BaseMeters> meters, int sectionCount)
        {
            this.routeID = routeID;
            this.type = type;
            this.number = number;
            this.times = times;
            this.meters = new List<BaseMeters>(meters);
            this.sectionCount = sectionCount;
        }
        public void ToAway(Action<int, string, string, BaseBackgroundTime, IEnumerable<BaseMeters>, int> instructionsForMe)
        {
            instructionsForMe(routeID, type, number, times, meters, sectionCount);
        }
        public TResult ToAway<TResult>(Func<int, string, string, BaseBackgroundTime, IEnumerable<BaseMeters>, int, TResult> instructions)
        {
            return instructions(routeID, type, number, times, meters, sectionCount);
        }

        public override string ToString()
        {
            return type + "-" + number;
        }

    }
}