using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Models
{
    public class TrainsDecorator : RouteDecorator
    {
        public TrainsDecorator(Route route) : base(route)
        {
        }
        public bool IsManueversWithoutHelper()
        {
            if (single)
            {
                foreach (var train in trains)
                {
                    var traindecorator = new RateTrainDecorator(train);
                    if (traindecorator.IsLocalFreight()) return true;
                }
                if (trains.Count == 0 && stations.Count == 1 && stations[0].OnlyMaeuvers()) return true;
                else return false;
            }
            else return false;
        }
        public TimeSpan CalcRailLubricatorTime()
        {
            var time = new TimeSpan();
            var previous = new RateTrainDecorator(new Train());
            foreach(var train in trains)
            {
                var traindecorator = new RateTrainDecorator(train);
                if (traindecorator.IsRailLubricator()) time += traindecorator.CalcLocomotivesServiceTime(previous);
                previous = traindecorator;
            }
            return time;
        }

    }
}
