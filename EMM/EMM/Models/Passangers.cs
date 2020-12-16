using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class Passangers: WorkTimesModel
    {
        public Passangers(DateTime start, DateTime finish, List<Passanger> passangers, List<Locomotive> locomotives) : base(start, finish, passangers, locomotives)
        {
        }
        public TimeSpan CalcNonWorkTime() => nonWorkTime;
        public TimeSpan CalcPassangersTime()
        {
            var time = CalcWorkTimeModelsPassTime();
            var startBackGroundTime = new TimeSpan();
            var finishBackGroundTime = new TimeSpan();
            if (firstPassanger == true) startBackGroundTime = unionWorkTime.First().Difference(start);
            if (lastPassanger == true) finishBackGroundTime = unionWorkTime.Last().FinishDifference(finish);
            return time + startBackGroundTime + finishBackGroundTime;
        }
        private TimeSpan CalcWorkTimeModelsPassTime()
        {
            TimeSpan time = new TimeSpan();
            foreach(var worktime in unionWorkTime)
            {
                time += worktime.CalcNonWorkTime();
            }
            return time;
        }

        public TimeSpan CalcWaitPassangersTime()
        {
            var passTime = CalcPassangersTime();
            var waitPassTime = nonWorkTime - passTime;
            if (waitPassTime.TotalHours < 0) return new TimeSpan();
            else return waitPassTime;
        }
    }
}
