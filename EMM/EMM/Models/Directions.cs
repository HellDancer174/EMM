using EMM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMM.Models
{
    public class Directions
    {
        private List<Direction> directions;
        public Directions(List<Direction> directions)
        {
            this.directions = directions;
        }
        public Tuple<string,string> ToDirection(List<string> stations)
        {
            var ourDirection = FindOurDirection(stations);
            return ourDirection.ToTechStations(stations);
        }
        private Direction FindOurDirection(List<string> stations)
        {
            try
            {
                return directions.Single(dir => dir.IsOwner(stations));
            }
            catch (InvalidOperationException)
            {
                return new Direction(new string[0], new string[0], String.Empty, String.Empty, String.Empty, String.Empty);
            }
        }
        public string ToReverseStation(List<string> stations)
        {
            var ourDirection = FindOurDirection(stations);
            return ourDirection.ReverseStation(stations);
        }
    }
}
