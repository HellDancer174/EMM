using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class StationsTimeCounter: IStationsTimeCounter
    {
        public double GetFullTime(IEnumerable<Station> stations)
        {
            if (stations.Count() == 0) return 0;
            var full = stations.Last().GetFullTime(stations.First());
            return full.TotalHours;
        }

        public double GetDownTime(IEnumerable<Station> stations)
        {
            var count = stations.Count();
            if (count == 0) return 0;
            return stations.Skip(1).Take(count - 2).Select(station => station.GetDownTime()).Sum(time => time.TotalHours);
        }
        public double GetFullTime(DateTime start, IEnumerable<Station> stations)
        {
            if (stations.Count() == 0) return 0;
            var fulltime = stations.Last().GetFullTime(start).TotalHours;
            if (fulltime < 0) return 0;
            else return fulltime;
        }
    }
}
