using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class LocomotiveComparer : IComparer<Locomotive>
    {
        private readonly DateTime start;

        public LocomotiveComparer(DateTime start)
        {
            this.start = start;
        }
        public int Compare(Locomotive x, Locomotive y)
        {
            return x.Difference(start).CompareTo(y.Difference(start));
        }
    }
}
