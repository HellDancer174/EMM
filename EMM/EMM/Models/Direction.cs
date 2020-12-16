using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMM.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Direction: DirectionFormatter
    {
        [JsonProperty]
        private string name;
        [JsonProperty]
        private string first;
        [JsonProperty]
        private string last;
        [JsonProperty]
        private readonly string[] stationsName;
        [JsonProperty]
        private readonly string[] localStations;
        [JsonProperty]
        private readonly string bigTechStation;
        [JsonProperty]
        private readonly string littleTechStation;


        public Direction(IEnumerable<string> stationsName, IEnumerable<string> localStations, string first, string last, string bigTechStation, string littleTechStation)
        {
            this.stationsName = stationsName.ToArray();
            this.localStations = localStations.ToArray();
            this.first = first;
            this.last = last;
            Rename();
            this.bigTechStation = bigTechStation; /*new List<string>() { "Кропачево", "Петропавловск", "Карталы", "Верхний Уфалей", "Екатеринбург", "Екатеринбург Пассажирский", "Екатеринбург Сортировочный" };*/
            this.littleTechStation = littleTechStation;/*new List<string>() { "Златоуст", "Курган", "Нижняя" } ;*/
        }

        private void Rename()
        {
            if (this.stationsName.Length == 0)
            {
                name = "Без имени";
                return;
            }
            if (first.StartsWith("Челябинск")) first = "Челябинск";
            if (last.StartsWith("Челябинск")) last = "Челябинск";
            if (first.StartsWith("Екатеринбург")) first = "Екатеринбург";
            if (last.StartsWith("Екатеринбург")) last = "Екатеринбург";
            name = GetDirection(first, last);
        }

        public void Reverse()
        {
            var temp = first;
            first = last;
            last = temp;
            Rename();
            stationsName.Reverse();
        }
        public override string ToString() => name;
        public bool IsOwner(IEnumerable<string> stations)
        {
            var countStations = stations.Count();
            if (countStations == 0) return false;
            var countDirectionsStations = stations.Select(station => stationsName.Contains(station)).Where(station => station == true).Count();
            var countLocalsStations = stations.Select(station => localStations.Contains(station)).Where(station => station == true).Count();
            if (countDirectionsStations == countStations && countDirectionsStations + countLocalsStations != countStations ||
                countDirectionsStations + countLocalsStations == countStations && countStations != countLocalsStations) return true;
            else return false;
        }
        public Tuple<string, string> ToTechStations(List<string> stations)
        {
            if (IsOwner(stations) == false) return Tuple.Create("", "");

            bool chelybinskFirst = stations.FirstOrDefault().StartsWith("Челябинск");
            var lastFlag = bigTechStation == stations.LastOrDefault() && chelybinskFirst;
            bool chelyabinskLast = stations.LastOrDefault().StartsWith("Челябинск");
            var firstFlag = bigTechStation == stations.FirstOrDefault() && chelyabinskLast;
            if (lastFlag || firstFlag) return Tuple.Create(stations.FirstOrDefault(), stations.LastOrDefault());
            //if (firstFlag) return GetDirection(stations.LastOrDefault(), stations.FirstOrDefault());

            //var smallTechStationsFlags = smallTechStations.Select(station => stations.Contains(station)).ToList();
            bool countains = stations.Contains(littleTechStation);
            if (countains && chelybinskFirst) return Tuple.Create(stations.FirstOrDefault(), littleTechStation);
            if(countains && chelyabinskLast) return Tuple.Create(littleTechStation, stations.LastOrDefault());

            return Tuple.Create("", "");
        }
        public string ReverseStation(List<string> stations)
        {
            if (IsOwner(stations) == false) return String.Empty;
            bool chelybinskFirst = stations.FirstOrDefault().StartsWith("Челябинск");
            bool chelyabinskLast = stations.LastOrDefault().StartsWith("Челябинск");
            if (!(chelybinskFirst || chelyabinskLast)) return String.Empty;
            else if (stations.Contains(bigTechStation)) return bigTechStation;
            else if (stations.Contains(littleTechStation)) return littleTechStation;
            else return String.Empty;
        }

    }
}