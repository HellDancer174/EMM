using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseBackgroundTime
    {
        protected int id;
        protected DateTime inspection;
        protected DateTime cPExit;
        protected DateTime cPEntrance;
        protected DateTime change;
        public BaseBackgroundTime()
        {

        }
        public BaseBackgroundTime(int id, DateTime inspection, DateTime cPExit, DateTime cPEntrance, DateTime change)
        {
            this.id = id;
            this.inspection = inspection;
            this.cPExit = cPExit;
            this.cPEntrance = cPEntrance;
            this.change = change;
        }
        public void ToAway(Action<int, DateTime, DateTime, DateTime, DateTime> instructions)
        {
            instructions(id,inspection, cPExit, cPEntrance, change);
        }
        public TResult ToAway<TResult>(Func<int, DateTime, DateTime, DateTime, DateTime, TResult> instructions)
        {
            return instructions(id, inspection, cPExit, cPEntrance, change);
        }

    }
}