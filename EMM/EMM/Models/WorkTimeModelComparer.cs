using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class WorkTimeModelComparer : IComparer<IWorkTimeModel>
    {
        private readonly DateTime start;

        public WorkTimeModelComparer(DateTime start)
        {
            this.start = start;
        }
        public int Compare(IWorkTimeModel x, IWorkTimeModel y)
        {
            return x.Difference(start).CompareTo(y.Difference(start));
        }
    }
}
