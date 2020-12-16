using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class PassangerComparer : IComparer<Passanger>
    {
        private readonly DateTime start;

        public PassangerComparer(DateTime start)
        {
            this.start = start;
        }
        public int Compare(Passanger x, Passanger y)
        {
            return x.Difference(start).CompareTo(y.Difference(start));
        }
    }
}
