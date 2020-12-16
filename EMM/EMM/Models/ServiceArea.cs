using EMM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class ServiceArea
    {
        private List<string> stations;
        private string first;
        private string last;
        private Directions directions;
        private GenericTryCatcher<string> catcher = new StringTryCatcher();
        public ServiceArea(List<string> stations, Directions directions)
        {
            this.stations = stations;
            this.directions = directions;
            first = catcher.Execute(stations.First);
            last = catcher.Execute(stations.Last);
        }
        public int ToDistance()
        {
            var distance = new RouteDistance(first, last);
            var distanceValue = distance.ToDistance();
            if(distanceValue == 0)
            {
                var reverseStation = directions.ToReverseStation(stations);
                if (reverseStation == String.Empty) return distanceValue;
                else
                {
                    bool chelFirst = first.StartsWith("Челябинск");
                    bool chelLast = last.StartsWith("Челябинск");
                    if (chelFirst && chelLast)
                    {
                        var fullReverseDistance = new RouteDistance("Челябинск", reverseStation);
                        return fullReverseDistance.ToDistance() * 2;
                    }
                    else if (chelFirst || chelLast)
                    {
                        var NotFullReverseDistance = new RouteDistance("Челябинск", reverseStation);
                        return NotFullReverseDistance.ToDistance();
                    }
                    else return distanceValue;
                }
            }
            else return distanceValue;
        }
    }
}
