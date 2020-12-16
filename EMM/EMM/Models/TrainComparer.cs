using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class TrainComparer : IComparer<Train>
    {
        private DateTime start;
        private IEnumerable<Station> stations;
        public TrainComparer(DateTime start, IEnumerable<Station> stations)
        {
            this.start = start;
            this.stations = stations;
        }
        public int Compare(Train x, Train y)
        {
            var xSubStationer = x.CreateSubStationer(stations);
            var ySubStationer = y.CreateSubStationer(stations);
            var xStations = xSubStationer.CutStations();
            var yStations = ySubStationer.CutStations();
            //if (xStations.Count() == 0 && yStations.Count() != 0) return -1;
            //else if (xStations.Count() != 0 && yStations.Count() == 0) return 1;
            return x.Difference(start, xStations).CompareTo(y.Difference(start, yStations));
        }
    }
}
