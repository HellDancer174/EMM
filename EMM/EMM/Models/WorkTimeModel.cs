using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class WorkTimeModel : IWorkTimeModel
    {
        private DateTime start;
        private DateTime finish;
        private readonly bool isWork;

        public WorkTimeModel(DateTime start, DateTime finish, bool isWork)
        {
            this.start = start;
            this.finish = finish;
            this.isWork = isWork;
        }
        public TimeSpan Difference(DateTime start)
        {
            return this.start - start;
        }
        public TimeSpan FinishDifference(DateTime finish)
        {
            return finish - this.finish;
        }
        public TimeSpan CalcWorkTime()
        {
            if (isWork)
            {
                return CalcTime();
            }
            else return default(TimeSpan);
        }

        public TimeSpan CalcTime(DateTime start)
        {
            var time = finish - start;
            if (time < default(TimeSpan)) return default(TimeSpan);
            else return time;
        }
        public TimeSpan CalcTime()
        {
            return CalcTime(this.start);
        }
        public TimeSpan CalcNonWorkTime()
        {
            if (isWork == false)
            {
                return CalcTime();
            }
            else return default(TimeSpan);
        }
        public bool IsWork()
        {
            return isWork;
        }
        //public double CalcWorkTime(List<WorkTimeModel> workTimes)
        //{
        //    var start = new DateTime();
        //    var finish = new DateTime();
        //    var startIndex = 0;
        //    var finishIndex = 0;
        //    for(int i = 0; i < workTimes.Count; i++)
        //    {
        //        if (workTimes[i].isWork == true)
        //        {
        //            start = workTimes[i].start;
        //            finish = workTimes[i].finish;
        //            startIndex = i;
        //            for (int j = startIndex; j < workTimes.Count; j++)
        //            {
        //                if()
        //            }
        //            //break;
        //        }
        //    }

            //foreach(var time in workTimes)
            //{
            //    if(time.isWork && startFlag)
            //    {
            //        start = time.start;
            //        startFlag = false;
            //        break;
            //    }
            //}

        //}
    }
}
