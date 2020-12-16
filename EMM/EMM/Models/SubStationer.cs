using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMM.Models
{
    public class SubStationer : ISubStationer
    {
        private List<Station> stations;
        private string arraval;
        private string depature;
        private int count;
        public SubStationer(IEnumerable<Station> stations, string arravalStation, string depatureStation)
        {
            this.stations = new List<Station>(stations);
            arraval = arravalStation;
            depature = depatureStation;
            count = CutStations().Count();
        }

        public IEnumerable<Station> CutStations()
        {
            var stationsName = stations.Select(station => station.ToString()).ToList();
            //arraval = ConvertStation(arraval, stationsName);
            //depature = ConvertStation(depature, stationsName);
            var arravalIndex = stationsName.IndexOf(arraval);
            var depatureIndex = stationsName.IndexOf(depature);
            if (depatureIndex == -1 || arravalIndex == -1) return new List<Station>();
            if (depatureIndex < arravalIndex)
            {
                var temp = depatureIndex;
                depatureIndex = arravalIndex;
                arravalIndex = temp;
            }
            var ourStations = stations.Skip(arravalIndex).Take(depatureIndex + 1 - arravalIndex).ToList();
            return ourStations;
        }

        //private string ConvertStation(string station, IEnumerable<string> stations)
        //{
        //    if (station == "Каменск-Уральский" || station.StartsWith("Екатеринбург"))
        //    {
        //        var newStation = stations.Where(element => element == "Нижняя").ToList();
        //        if (newStation.Count() == 0) return station;
        //        else return newStation.First();
        //    }
        //    else return station;
        //}



    }
}
